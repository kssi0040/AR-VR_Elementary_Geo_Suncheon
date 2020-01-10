using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz_XML_Reader : MonoBehaviour
{
    private static Quiz_XML_Reader instance = null;
    private static readonly object padlock = new object();

    [HideInInspector]
    public bool readCompleted;

    private Quiz_XML_Reader()
    {

    }

    public static Quiz_XML_Reader Instance
    {
        get
        {
            lock(padlock)
            {
                if(instance==null)
                {
                    instance = new Quiz_XML_Reader();
                }
                return instance;
            }
        }
    }

    string fileName_2 = "Social_Quiz_2.xml";
    private string filePath = string.Empty;

    public struct SQuiz
    {
        public int stage;
        public int id;
        public string Text;
        public string Type;
        public string Order;
        public string Answer;
    }

    public struct Quiz
    {
        public List<int> Num;
        public List<String> text;
        public List<String> Answer;

        public Quiz(List<int> num, List<String> _text, List<String> _Answer)
        {
            num = new List<int>();
            _text = new List<string>();
            _Answer = new List<string>();

            Num = num;
            text = _text;
            Answer = _Answer;
        }
    }

    public List<Quiz> quiz = new List<Quiz>();

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        filePath += ("file:///");
        filePath += (Application.streamingAssetsPath + "/" + fileName_2);
#elif UNITY_ANDROID
        filePath += Application.streamingAssetsPath+"/"+fileName_2;
#endif
    }
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Process());

        readCompleted = true;

        /*
        for (int i = 0; i < quiz.Count; i++)
        {
            for (int j = 0; j < quiz[i].Num.Count; j++)
            {
                Debug.Log("ID: " + quiz[i].Num[j] + " Text: " + quiz[i].text[j]);        
            }
        }
         * */

        for (int key = 0; key < OptionQuizDict.Count; key++)
	    {
            Debug.Log("OptionQuizDict: " + OptionQuizDict[key].stage + " "
                + OptionQuizDict[key].id + " "
                + OptionQuizDict[key].Text + " "
                + OptionQuizDict[key].Type + " "
                + OptionQuizDict[key].Order + " "
                + OptionQuizDict[key].Answer);
	    }

        for (int key = 0; key < Typing1QuizDict.Count; key++)
        {
            Debug.Log("Typing1QuizDict: " + Typing1QuizDict[key].stage + " "
                + Typing1QuizDict[key].id + " "
                + Typing1QuizDict[key].Text + " "
                + Typing1QuizDict[key].Type + " "
                + Typing1QuizDict[key].Order + " "
                + Typing1QuizDict[key].Answer);
        }

        for (int key = 0; key < Typing2QuizDict.Count; key++)
        {
            Debug.Log("Typing2QuizDict: " + Typing2QuizDict[key].stage + " "
                + Typing2QuizDict[key].id + " "
                + Typing2QuizDict[key].Text + " "
                + Typing2QuizDict[key].Type + " "
                + Typing2QuizDict[key].Order + " "
                + Typing2QuizDict[key].Answer);
        }

        for (int key = 0; key < LinkQuizDict.Count; key++)
        {
            Debug.Log("LinkQuizDict: " + LinkQuizDict[key].stage + " "
                + LinkQuizDict[key].id + " "
                + LinkQuizDict[key].Text + " "
                + LinkQuizDict[key].Type + " "
                + LinkQuizDict[key].Order + " "
                + LinkQuizDict[key].Answer);
        }
	}

    IEnumerator Process()
    {
        WWW www = new WWW(filePath);
        
        yield return www;

        interpret2(www.text);
    }

    public List<SQuiz> LinkQuizDict = new List<SQuiz>();
    public List<SQuiz> Typing1QuizDict = new List<SQuiz>();
    public List<SQuiz> Typing2QuizDict = new List<SQuiz>();
    public List<SQuiz> OptionQuizDict = new List<SQuiz>();

    private void interpret2(string _strSource)
    {
        StringReader stringReader = new StringReader(_strSource);

        XmlNodeList xmlNodeList = null;

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(stringReader.ReadToEnd());

        xmlNodeList = xmlDoc.SelectNodes("Social_Quiz");

        foreach (XmlNode node in xmlNodeList)
        {
            if (node.Name.Equals("Social_Quiz") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Attributes.GetNamedItem("id").Value != "*")
                    {
                        if (child.Attributes.GetNamedItem("Type").Value == "Option")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;
                            squiz.Order = child.Attributes.GetNamedItem("Order").Value;
                            squiz.Answer = child.Attributes.GetNamedItem("answer").Value;

                            OptionQuizDict.Add(squiz);

                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "Typing1")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;
                            squiz.Order = child.Attributes.GetNamedItem("Order").Value;
                            squiz.Answer = child.Attributes.GetNamedItem("answer").Value;

                            Typing1QuizDict.Add(squiz);
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "Typing2")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;
                            squiz.Order = child.Attributes.GetNamedItem("Order").Value;
                            squiz.Answer = child.Attributes.GetNamedItem("answer").Value;

                            Typing2QuizDict.Add(squiz);
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "Link")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;
                            squiz.Order = child.Attributes.GetNamedItem("Order").Value;
                            squiz.Answer = child.Attributes.GetNamedItem("answer").Value;

                            LinkQuizDict.Add(squiz);
                        }
                    }
                    
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
