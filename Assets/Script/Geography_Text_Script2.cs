using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Geography_Text_Script2 : MonoBehaviour
{
    private static Geography_Text_Script2 instance = null;
    private static readonly object padlock = new object();

    [HideInInspector]
    public bool readCompleted;
    private Geography_Text_Script2()
    {

    }

    // 인스턴스화
    public static Geography_Text_Script2 Instance
    {
        get
        {
            // lock 키워드는 특정 블럭의 코드를 한번에 하나의 쓰레드만 실행할 수 있도록 해준다.
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Geography_Text_Script2();
                }
                return instance;
            }
        }
    }

    string fileName = "Geography_Text3.xml";
    public string filePath = string.Empty;  //비우는거 string.Empty == null

    // 생성자를 쓰기위한 과정
    public struct Scenario
    {
        public List<int> Num;//id
        public List<String> text;//text
        public List<String> cha; //char
        public List<String> anim; //anim
        public List<String> backImg;
        public List<String> type;
        public List<String> who;

        //생성자
        public Scenario(List<int> num, List<String> _text, List<String> _cha, List<String> _anim, List<String> _backImg, List<String> _type, List<String> _who)
        {
            num = new List<int>();
            _text = new List<string>();
            _cha = new List<string>();
            _anim = new List<string>();
            _backImg = new List<string>();
            _type = new List<string>();
            _who = new List<string>();

            Num = num;
            text = _text;
            cha = _cha;
            anim = _anim;
            backImg = _backImg;
            type = _type;
            who = _who;
        }
    }

    public List<Scenario> scenario = new List<Scenario>();

    // 초기에 Awake에서 파일의 위치를 읽어들이는 과정(파일위치는 공통되어있음)
    private void Awake()
    {


        //Scenario temp = new Scenario(new List<int>(), new List<string>());

        //for(int k=0;k<5; k++)
        //{
        //    temp.Num.Add(k);
        //    temp.text.Add("Test" + temp.Num[k].ToString());
        //}

        //scenario.Add(temp);




        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        //filePath += "jar:file://" + Application.streamingAssetsPath + "!/assets/" + fileName;
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        filePath += ("file:///");
        filePath += (Application.streamingAssetsPath + "/" + fileName);
#elif UNITY_ANDROID
        filePath += Application.streamingAssetsPath+"/"+fileName;
#endif
        readCompleted = false;
        //Debug.Log(scenario[0].text[scenario[0].Num[3]]);
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Process());

        for (int i = 0; i < scenario.Count; i++)
        {
            for (int j = 0; j < scenario[i].Num.Count; j++)
            {
                Debug.Log("Language_Text_Id :" + scenario[i].Num[j]);
            }
            Debug.Log("-----------------");

            for (int j = 0; j < scenario[i].text.Count; j++)
            {
                Debug.Log("Language_Text_Text :" + scenario[i].text[j]);
            }
            Debug.Log("-----------------");

            for (int j = 0; j < scenario[i].cha.Count; j++)
            {
                Debug.Log("Language_Text_Char :" + scenario[i].cha[j]);
            }
            Debug.Log("-----------------");

            for (int j = 0; j < scenario[i].anim.Count; j++)
            {
                Debug.Log("Language_Text_Anim :" + scenario[i].anim[j]);
            }
            Debug.Log("-----------------");

            for (int j = 0; j < scenario[i].backImg.Count; j++)
            {
                Debug.Log("Language_Text_BackImg :" + scenario[i].backImg[j]);
            }
            Debug.Log("-----------------");

            for (int j = 0; j < scenario[i].type.Count; j++)
            {
                Debug.Log("Language_Text_Type :" + scenario[i].type[j]);
            }
            Debug.Log("-----------------");

            //for (int j = 0; j < scenario[i].who.Count; j++)
            //{
            //    Debug.Log("Language_Text_Who :" + scenario[i].who[j]);
            //}
            //Debug.Log("-----------------");
        }
    }

    // Coroutine함수의 WWW클래스를 통해 파일의 내용을 읽어들이는 과정
    IEnumerator Process()
    {
        WWW www = new WWW(filePath);
        yield return www;
        if (www.isDone)
        {
            interpret(www.text);
            readCompleted = true;
        }
    }

    //실제 읽는 부분
    private void interpret(string _strSource)       //interpret(www.text)
    {
        StringReader stringReader = new StringReader(_strSource);

        //stringReader.Read();
        XmlNodeList xmlNodeList = null;             //XmlNodeLIst. XmlDocument

        XmlDocument xmlDoc = new XmlDocument();     //Xml을 선언

        xmlDoc.LoadXml(stringReader.ReadToEnd());   //File에 있는 내용을 모두 한 번에 읽어와서 반환 (읽어온 data에서 필요한 부분을 추출하여 필요한 곳에 활용)


        xmlNodeList = xmlDoc.SelectNodes("Text_XML");



        int index = 0;
        List<int> tempNum = new List<int>();            //임시적으로 넣어주기 위한 리스트
        List<String> tempText = new List<string>();     //임시적으로 넣어주기 위한 리스트
        List<String> tempCha = new List<string>();
        List<String> tempAnim = new List<string>();
        List<String> tempBackImg = new List<string>();
        List<String> tempType = new List<string>();
        List<String> tempWho = new List<string>();

        foreach (XmlNode node in xmlNodeList)
        {
            if (node.Name.Equals("Text_XML") && node.HasChildNodes)
            {
                // 아래의 자식 노드 반복
                foreach (XmlNode child in node.ChildNodes)
                {
                    //stage == *에 도달하면
                    if (child.Attributes.GetNamedItem("stage").Value == "*")
                    {
                        Debug.Log("임의 List의 id의 수: " + tempNum.Count.ToString());
                        Debug.Log("임의 List의 text의 수: " + tempText.Count.ToString());
                        Debug.Log("임의 List의 cha의 수: " + tempCha.Count.ToString());
                        Debug.Log("임의 List의 anim의 수: " + tempAnim.Count.ToString());
                        Debug.Log("임의 List의 backImg의 수: " + tempBackImg.Count.ToString());
                        Debug.Log("임의 List의 type의 수: " + tempType.Count.ToString());
                        Debug.Log("임의 List의 who의 수: " + tempWho.Count.ToString());

                        scenario.Add(new Scenario(new List<int>(), new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>()));

                        //id와 text를 불어들이기 위한 for문 여러가지
                        // scenario가 index++로 자동적으로 늘어나서 stage를 자동적으로 구분
                        for (int k = 0; k < tempNum.Count; k++)
                        {
                            scenario[index].Num.Add(tempNum[k]);
                        }
                        for (int j = 0; j < tempText.Count; j++)
                        {
                            scenario[index].text.Add(tempText[j]);
                        }
                        for (int l = 0; l < tempCha.Count; l++)
                        {
                            scenario[index].cha.Add(tempCha[l]);
                        }
                        for (int m = 0; m < tempAnim.Count; m++)
                        {
                            scenario[index].anim.Add(tempAnim[m]);
                        }
                        for (int n = 0; n < tempBackImg.Count; n++)
                        {
                            scenario[index].backImg.Add(tempBackImg[n]);
                        }
                        for (int b = 0; b < tempType.Count; b++)
                        {
                            scenario[index].type.Add(tempType[b]);
                        }
                        for (int v = 0; v < tempWho.Count; v++)
                        {
                            scenario[index].who.Add(tempWho[v]);
                        }

                        Debug.Log("KBY!!!저장되어야할 변수의 List의 id의 수: " + scenario[scenario.Count - 1].Num.Count.ToString());
                        Debug.Log("KBY!!!저장되어야할 변수의 List의 text의 수: " + scenario[scenario.Count - 1].text.Count.ToString());
                        Debug.Log("KBY!!!저장되어야할 변수의 List의 cha의 수: " + scenario[scenario.Count - 1].cha.Count.ToString());
                        Debug.Log("KBY!!!저장되어야할 변수의 List의 anim의 수: " + scenario[scenario.Count - 1].anim.Count.ToString());
                        Debug.Log("KBY!!!저장되어야할 변수의 List의 backImg의 수: " + scenario[scenario.Count - 1].backImg.Count.ToString());
                        Debug.Log("KBY!!!저장되어야할 변수의 List의 type의 수: " + scenario[scenario.Count - 1].type.Count.ToString());
                        Debug.Log("KBY!!!저장되어야할 변수의 List의 who의 수: " + scenario[scenario.Count - 1].who.Count.ToString());
                        index++;
                        tempNum.Clear();
                        tempText.Clear();
                        tempCha.Clear();
                        tempAnim.Clear();
                        tempBackImg.Clear();
                        tempType.Clear();
                        tempWho.Clear();
                    }
                    // * 에 도달하기 전까지 임시적으로 값을 계속해서 넣어줌
                    else
                    {
                        tempNum.Add(int.Parse(child.Attributes.GetNamedItem("id").Value));
                        tempText.Add(child.Attributes.GetNamedItem("Text").Value);
                        tempCha.Add(child.Attributes.GetNamedItem("char").Value);
                        tempAnim.Add(child.Attributes.GetNamedItem("anim").Value);
                        tempBackImg.Add(child.Attributes.GetNamedItem("backImg").Value);
                        tempType.Add(child.Attributes.GetNamedItem("type").Value);
                        tempWho.Add(child.Attributes.GetNamedItem("who").Value);
                    }
                }
            }
        }
        Debug.Log(scenario.Count.ToString());

        //Debug.Log("text: " + scenario[0].cha[3]);



    }
    // Update is called once per frame
    void Update()
    {

    }
}
