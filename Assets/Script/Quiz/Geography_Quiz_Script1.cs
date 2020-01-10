using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Geography_Quiz_Script1 : MonoBehaviour
{
    private static Geography_Quiz_Script1 instance = null;
    private static readonly object padlock = new object();

    [HideInInspector]
    public bool readCompleted;

    private Geography_Quiz_Script1()
    {

    }

    public static Geography_Quiz_Script1 Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Geography_Quiz_Script1();
                }
                return instance;
            }
        }
    }

    //string fileName = "Social_Quiz.xml";
    string fileName_2 = "Geography_Quiz1.xml";        //파일이름 넣기
    private string filePath = string.Empty;

    public struct SQuiz
    {
        public int stage;
        public int id;
        public string Text;
        public string Type;
        public List<string> Order;
        public List<string> Answer;
    }

    // 파일경로 탐색후 값 읽어오기
    private void Awake()
    {
        if (instance != null)
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
        readCompleted = false;
        Debug.Log("KBY !!!: " + filePath);
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Process());


        Debug.Log("readCompleted: " + readCompleted);

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
                + OptionQuizDict[key].Text + "\n"
                + OptionQuizDict[key].Type + " ");
            for (int k = 0; k < OptionQuizDict[key].Order.Count; k++)
            {
                Debug.Log(OptionQuizDict[key].Order[k] + " ");
            }
            for (int i = 0; i < OptionQuizDict[key].Answer.Count; i++)
            {
                Debug.Log(OptionQuizDict[key].Answer[i]);
            }


        }
        for (int i = 0; i < ImgOptionQuizDict.Count; i++)
        {
            Debug.Log("ImgOptionQuizDict : " + ImgOptionQuizDict[i].stage + " "
                + ImgOptionQuizDict[i].id + " "
                + ImgOptionQuizDict[i].Text + "\n"
                + ImgOptionQuizDict[i].Type + " ");
            for (int j = 0; j < ImgOptionQuizDict[i].Order.Count; j++)
            {
                Debug.Log(ImgOptionQuizDict[i].Order[j] + " ");
            }
            for (int k = 0; k < ImgOptionQuizDict[i].Answer.Count; k++)
            {
                Debug.Log(ImgOptionQuizDict[i].Answer[k]);
            }
        }

        for (int key = 0; key < Typing1QuizDict.Count; key++)
        {
            Debug.Log("Typing1QuizDict: " + Typing1QuizDict[key].stage + " "
                + Typing1QuizDict[key].id + " "
                + Typing1QuizDict[key].Text + "\n"
                + Typing1QuizDict[key].Type + " ");
            for (int k = 0; k < Typing1QuizDict[key].Order.Count; k++)
            {
                Debug.Log(Typing1QuizDict[key].Order[k] + " ");
            }
            for (int i = 0; i < Typing1QuizDict[key].Answer.Count; i++)
            {
                Debug.Log(Typing1QuizDict[key].Answer[i]);
            }
        }

        for (int key = 0; key < Typing2QuizDict.Count; key++)
        {
            Debug.Log("Typing2QuizDict: " + Typing2QuizDict[key].stage + " "
           + Typing2QuizDict[key].id + " "
           + Typing2QuizDict[key].Text + "\n"
           + Typing2QuizDict[key].Type + " ");
            for (int k = 0; k < Typing2QuizDict[key].Order.Count; k++)
            {
                Debug.Log(Typing2QuizDict[key].Order[k] + " ");
            }
            for (int i = 0; i < Typing2QuizDict[key].Answer.Count; i++)
            {
                Debug.Log(Typing2QuizDict[key].Answer[i]);
            }
        }

        for (int key = 0; key < LinkQuizDict.Count; key++)
        {
            Debug.Log("LinkQuizDict: " + LinkQuizDict[key].stage + " "
            + LinkQuizDict[key].id + " "
            + LinkQuizDict[key].Text + "\n"
            + LinkQuizDict[key].Type + " ");
            for (int k = 0; k < LinkQuizDict[key].Order.Count; k++)
            {
                Debug.Log(LinkQuizDict[key].Order[k] + " ");
            }
            for (int i = 0; i < LinkQuizDict[key].Answer.Count; i++)
            {
                Debug.Log(LinkQuizDict[key].Answer[i]);
            }
        }
        for (int i = 0; i < Link2QuizDict.Count; i++)
        {
            Debug.Log("Link2QuizDict : " + Link2QuizDict[i].stage + " "
                + Link2QuizDict[i].id + " "
                + Link2QuizDict[i].Text + "\n"
                + Link2QuizDict[i].Type + " ");
            for (int j = 0; j < Link2QuizDict[i].Order.Count; j++)
            {
                Debug.Log(Link2QuizDict[i].Order[j] + " ");
            }
            for (int k = 0; k < Link2QuizDict[i].Answer.Count; k++)
            {
                Debug.Log(Link2QuizDict[i].Answer[k]);
            }
        }
        for (int i = 0; i < puzzleQuizDict.Count; i++)
        {
            Debug.Log("puzzleQuizDict : " + puzzleQuizDict[i].stage + " "
                + puzzleQuizDict[i].id + " "
                + puzzleQuizDict[i].Text + "\n"
                + puzzleQuizDict[i].Type + " ");
            for (int j = 0; j < puzzleQuizDict[i].Order.Count; j++)
            {
                Debug.Log(puzzleQuizDict[i].Order[j] + " ");
            }
            for (int k = 0; k < puzzleQuizDict[i].Answer.Count; k++)
            {
                Debug.Log(puzzleQuizDict[i].Answer[k]);
            }
        }
    }

    //Coroutine 실행
    IEnumerator Process()
    {
        WWW www = new WWW(filePath);
        //interpret(www.text);


        Debug.Log("KBY@@@@: " + www.text);
        yield return www;
        if (www.isDone)
        {
            interpret2(www.text);
            readCompleted = true;
        }
    }

    public List<SQuiz> LinkQuizDict = new List<SQuiz>();
    public List<SQuiz> Link2QuizDict = new List<SQuiz>();       // 순서대로 나열하는 링크(과학부분)
    public List<SQuiz> ImgLinkQuizDict = new List<SQuiz>();     // 이미지 링크리스트 추가
    public List<SQuiz> puzzleQuizDict = new List<SQuiz>();      // 퍼즐 링크 추가
    public List<SQuiz> Typing1QuizDict = new List<SQuiz>();
    public List<SQuiz> Typing2QuizDict = new List<SQuiz>();
    public List<SQuiz> OptionQuizDict = new List<SQuiz>();
    public List<SQuiz> ImgOptionQuizDict = new List<SQuiz>();   // 이미지 객관식 추가

    private void interpret2(string _strSource)
    {
        Debug.Log("KBY_1: " + _strSource);
        StringReader stringReader = new StringReader(_strSource);

        XmlNodeList xmlNodeList = null;

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(stringReader.ReadToEnd());

        //부모 노드명 맞추기
        xmlNodeList = xmlDoc.SelectNodes("Social_Quiz");

        foreach (XmlNode node in xmlNodeList)
        {
            //부모 노드명 맞추기
            if (node.Name.Equals("Social_Quiz") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    // * 가 아니면 데이터값 게속 넣기
                    if (child.Attributes.GetNamedItem("id").Value != "*")
                    {
                        if (child.Attributes.GetNamedItem("Type").Value == "Option")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<String>();
                            squiz.Answer = new List<String>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            string optOrder = child.Attributes.GetNamedItem("Order").Value;
                            string optAnswer = child.Attributes.GetNamedItem("answer").Value;

                            string[] optOrder_str = optOrder.Split(',');
                            string[] optAnswer_str = optAnswer.Split(',');

                            for (int i = 0; i < optOrder_str.Length; i++)
                            {
                                squiz.Order.Add(optOrder_str[i]);
                            }
                            for (int i = 0; i < optAnswer_str.Length; i++)
                            {
                                squiz.Answer.Add(optAnswer_str[i]);
                            }

                            OptionQuizDict.Add(squiz);
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "imgOption")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<String>();
                            squiz.Answer = new List<String>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            string _order = child.Attributes.GetNamedItem("Order").Value;
                            string _answer = child.Attributes.GetNamedItem("answer").Value;

                            string[] _answerArr = _answer.Split(',');
                            string[] _orderArr = _order.Split(',');

                            for (int i = 0; i < _orderArr.Length; i++)
                            {
                                squiz.Order.Add(_orderArr[i]);

                                Debug.Log("squiz.Order[i]: " + squiz.Order[i]);
                            }
                            for (int i = 0; i < _answerArr.Length; i++)
                            {
                                squiz.Answer.Add(_answerArr[i]);
                                Debug.Log("Asnsdwer : " + squiz.Answer[i]);
                            }

                            ImgOptionQuizDict.Add(squiz);
                        }


                        else if (child.Attributes.GetNamedItem("Type").Value == "Typing1")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<String>();
                            squiz.Answer = new List<String>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            //string typTmp_O = string.Empty;
                            //string typTmp_A = string.Empty;

                            ////한 글자씩 찍는 것이 아니게 하기 위해 string에 행의 문자길이만큼 추가
                            //foreach (char typOrder1 in child.Attributes.GetNamedItem("Order").Value.ToCharArray())
                            //{
                            //    typTmp_O += typOrder1.ToString();
                            //}

                            //foreach (char txt in child.Attributes.GetNamedItem("answer").Value.ToCharArray())
                            //{
                            //    typTmp_A += txt.ToString();
                            //}

                            ////구조체 squiz의 order와 answer에 각각 추가
                            //squiz.Order.Add(typTmp_O);
                            //squiz.Answer.Add(typTmp_A);
                            string typTmp_O = child.Attributes.GetNamedItem("Order").Value;
                            string typTmp_A = child.Attributes.GetNamedItem("answer").Value;

                            squiz.Order.Add(typTmp_O);
                            squiz.Answer.Add(typTmp_A);

                            Typing1QuizDict.Add(squiz);
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "Typing2")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<String>();
                            squiz.Answer = new List<String>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            string typ2Tmp_O = child.Attributes.GetNamedItem("Order").Value;
                            string typ2Tmp_A = child.Attributes.GetNamedItem("answer").Value;

                            squiz.Order.Add(typ2Tmp_O);
                            squiz.Answer.Add(typ2Tmp_A);

                            Typing2QuizDict.Add(squiz);
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "Link")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<string>();
                            squiz.Answer = new List<string>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            string linkTmp_O = child.Attributes.GetNamedItem("Order").Value;
                            string linkTmp_A = child.Attributes.GetNamedItem("answer").Value;

                            squiz.Order.Add(linkTmp_O);
                            squiz.Answer.Add(linkTmp_A);

                            LinkQuizDict.Add(squiz);
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "Link2")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<String>();
                            squiz.Answer = new List<String>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            string link2_order = child.Attributes.GetNamedItem("Order").Value;
                            string link2_answer = child.Attributes.GetNamedItem("answer").Value;
                            squiz.Order.Add(link2_order);
                            squiz.Answer.Add(link2_answer);

                            Link2QuizDict.Add(squiz);
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "imgLink")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<String>();
                            squiz.Answer = new List<string>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            string imgLink_Order = child.Attributes.GetNamedItem("Order").Value;
                            string imgLink_Answer = child.Attributes.GetNamedItem("answer").Value;

                            string[] _imgLinkStr1 = imgLink_Order.Split(',');
                            string[] _imgLinkStr2 = imgLink_Answer.Split(',');

                            for (int i = 0; i < _imgLinkStr1.Length; i++)
                            {
                                squiz.Order.Add(_imgLinkStr1[i]);
                            }
                            for (int i = 0; i < _imgLinkStr2.Length; i++)
                            {
                                squiz.Answer.Add(_imgLinkStr2[i]);
                            }
                        }
                        else if (child.Attributes.GetNamedItem("Type").Value == "puzzle")
                        {
                            SQuiz squiz = new SQuiz();
                            squiz.Order = new List<String>();
                            squiz.Answer = new List<string>();

                            squiz.stage = int.Parse(child.Attributes.GetNamedItem("stage").Value);
                            squiz.id = int.Parse(child.Attributes.GetNamedItem("id").Value);
                            squiz.Text = child.Attributes.GetNamedItem("Text").Value;
                            squiz.Type = child.Attributes.GetNamedItem("Type").Value;

                            string _puzzle_Order = child.Attributes.GetNamedItem("Order").Value;
                            string _puzzle_Answer = child.Attributes.GetNamedItem("answer").Value;

                            string[] puzzOrder_str = _puzzle_Order.Split(',');
                            string[] puzzAnswer_str = _puzzle_Answer.Split(',');

                            for (int i = 0; i < puzzOrder_str.Length; i++)
                            {
                                squiz.Order.Add(puzzOrder_str[i]);
                            }
                            for (int i = 0; i < puzzAnswer_str.Length; i++)
                            {
                                squiz.Answer.Add(puzzAnswer_str[i]);
                            }
                            puzzleQuizDict.Add(squiz);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


