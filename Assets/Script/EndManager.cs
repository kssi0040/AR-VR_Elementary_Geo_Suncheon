using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public string text;
    public RawImage TextBox;
    public Text str;
    public Transform CH1_nomal;     //여주인공 노말
    public Transform CH2_nomal;     //남주인공 노말
    public Transform CH3;           //선생(해설)
    public Animator Character;
    public Animator bird;           //눈새
    public int Stage;
    public int count;
    // Use this for initialization
    void Start()
    {
        count = 0;
        CH1_nomal.transform.gameObject.SetActive(false);
        CH2_nomal.transform.gameObject.SetActive(false);
        CH3.transform.gameObject.SetActive(false);
        bird.transform.gameObject.SetActive(false);

        // 해당 씬에 id값인 case를 비교한 뒤 기능 설정을 해주는 switch문(애니메이션 설정)
        switch (Stage)
        {
            case 0:
                {
                    //CH3.transform.gameObject.SetActive(true);   //시작 프롤로그 id  0번째 부분 시작은 해설이므로 선생오브젝트 활성화
                    //break;      //break로 case문을 빠져나가서 텍스트를 뿌려주는 switch문으로
                    if (AppManage.Instance.Gender == 0)
                    {
                        
                        CH2_nomal.transform.gameObject.SetActive(true);
                        Character.SetTrigger("CH2Anim");
                    }
                    else
                    {

                        CH1_nomal.transform.gameObject.SetActive(true);
                        Character.SetTrigger("CH1Anim");
                    }
                    
                }
                break;
            case 2:
            case 6:
                {
                    CH3.transform.gameObject.SetActive(true);                 
                }
                break;
            default:
                break;
        }

        // 위의 id값 마다 씬의 기능 설정을 하는 switch문 후에 break로 빠져 나온 뒤 해당 씬에 텍스트를 뿌려주는 switch문
        switch (Stage)
        {
            case 0:
            case 6:
                text = Geography_Text_Script2.Instance.scenario[Stage].text[count];     //해당 씬의 카운트 값인 텍스트를 불러옴
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);   //TypingManager로 인해 Text가 자연스럽게 뿌려짐
                }
                else
                {
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                }
                break;

            default:
                ChangeStr(Stage);
                break;
        }
        AppManage.Instance.isComplite = false;
    }
	
	// Update is called once per frame
	void Update () {

    }
    // 주인공의 이름이 ###일때 입력 후 텍스트를 뿌리기, 아닐 경우 텍스트만
    void ChangeStr(int _stage)
    {
        //Debug.Log(Text_XML_Reader.Instance.scenario[_stage].text[Text_XML_Reader.Instance.scenario[_stage].text.Count - 1]);
        // 0부터 시작하지만 1개부터 시작해서 수를 측정했기 때문에 해당 스테이지의 text 수의 Count-1값 만큼 셈
        text = Geography_Text_Script2.Instance.scenario[_stage].text[Geography_Text_Script2.Instance.scenario[_stage].text.Count - 1];

        if (text.Contains("###"))
        {
            string temp = text.Replace("###", AppManage.Instance.Name);
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
        }
        else
        {
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
        }
    }

    public void NextScript()    //버튼을 눌렀을 때 (화살표를 눌렀을때 호출)
    {
        if (AppManage.Instance.isComplite)
        {
            switch (Stage)
            {
                case 0:
                    {
                        AppManage.Instance.isComplite = false;
                        SceneManager.LoadScene("SelectMap");
                        break;
                    }
                case 6:
                    switch(count)
                    {
                        case 1:
                            AppManage.Instance.isComplite = false;
                            TextBox.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            AppManage.Instance.isExit = true;
                            count++;
                            break;
                        default:
                            CH1_nomal.transform.gameObject.SetActive(false);
                            CH2_nomal.transform.gameObject.SetActive(false);
                            CH3.transform.gameObject.SetActive(true);
                            bird.transform.gameObject.SetActive(false);
                            Character.SetTrigger("teachAnim");
                            count++;
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (Stage == 6)
            {
                if (count != 2)
                {
                    text = Geography_Text_Script2.Instance.scenario[Stage].text[Geography_Text_Script2.Instance.scenario[Stage].Num[count]];
                    if (text.Contains("###"))
                    {
                        string temp = text.Replace("###", AppManage.Instance.Name);
                        GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                    }
                    else
                    {
                        GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                    }
                    AppManage.Instance.isComplite = false;
                }
            }
            else
            {
                text = Geography_Text_Script2.Instance.scenario[Stage].text[Geography_Text_Script2.Instance.scenario[Stage].Num[count]];
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                }
                else
                {
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                }
                AppManage.Instance.isComplite = false;
            }
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
    }
}
