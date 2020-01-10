using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EventManager : MonoBehaviour
{
    public Image PopUp;
    public RectTransform button;
    public TextManager manager;
    public bool ishit = false;
	// Use this for initialization
	void Start ()
    {
        button = GameObject.Find("Next").GetComponent<RectTransform>();
        button.transform.gameObject.SetActive(false);
	}
	
    public void OnPointerDown()
    {
        Sprite HitTaiko = Resources.Load<Sprite>("Drum_Take_02");
        PopUp = GameObject.Find("PopUp").GetComponent<Image>();
        PopUp.overrideSprite = HitTaiko;
        button.transform.gameObject.SetActive(true);
        ishit = true;
        AppManage.Instance.isComplite = false;
    }

    public void OnPointerUp()
    {
        Sprite HitTaiko = Resources.Load<Sprite>("Drum_Take_01");
        PopUp = GameObject.Find("PopUp").GetComponent<Image>();
        PopUp.overrideSprite = HitTaiko;
    }

    public void Next()
    {
        TextManager manager = GameObject.Find("TextManager").GetComponent<TextManager>();
        RectTransform Hit = GameObject.Find("Hit").GetComponent<RectTransform>();
        Hit.transform.gameObject.SetActive(false);
        button.transform.gameObject.SetActive(false);
        Image BG =GameObject.Find("BackGround").GetComponent<Image>();
        BG.overrideSprite = Resources.Load<Sprite>("Demo_BG");
        manager.count++;
        Text str = GameObject.Find("str").GetComponent<Text>();
        string text = Text_XML_Reader.Instance.scenario[manager.currentStage].text[Text_XML_Reader.Instance.scenario[manager.currentStage].Num[manager.count]];
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

    public void Next2()
    {
        button.transform.gameObject.SetActive(false);
        manager.TextBox.transform.gameObject.SetActive(true);
        Text str = GameObject.Find("str").GetComponent<Text>();
        string text = Text_XML_Reader.Instance.scenario[manager.currentStage].text[Text_XML_Reader.Instance.scenario[manager.currentStage].Num[manager.count]];
        if (text.Contains("###"))
        {
            string temp = text.Replace("###", AppManage.Instance.Name);
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
        }
        else
        {
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
        }
        ishit = true;
    }

    public void Next3()
    {
        if (AppManage.Instance.isComplite)
        {
            manager.ThrowManager.isThrown = false;
            if (AppManage.Instance.Gender == 0)
            {
                manager.CH1_nomal.gameObject.SetActive(false);
                manager.CH2_nomal.gameObject.SetActive(true);
                manager.CH3.gameObject.SetActive(false);
            }
            else
            {
                manager.CH1_nomal.gameObject.SetActive(true);
                manager.CH2_nomal.gameObject.SetActive(false);
                manager.CH3.gameObject.SetActive(false);
            }
            manager.count++;
            manager.Heart.transform.GetChild(0).gameObject.SetActive(true);
            manager.Heart.transform.GetChild(1).gameObject.SetActive(true);
            string text = Geography_Text_Script2.Instance.scenario[manager.currentStage].text[Geography_Text_Script2.Instance.scenario[manager.currentStage].Num[manager.count]];
            if (manager.count == 16)
            {
                manager.blackBird.SetTrigger("BlackBird_Eat_seed");
                manager.str.text = string.Empty;
                manager.TextBox.transform.gameObject.SetActive(false);
                manager.NPCText.transform.gameObject.SetActive(true);
                manager.NPCTextBox.transform.gameObject.SetActive(true);
                manager.Food_Field1.transform.gameObject.SetActive(true);
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, manager.NPCText);
                }
                else
                {
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, manager.NPCText);
                }
            }
            else if(manager.count==20)
            {
                manager.blackBird.SetTrigger("BlackBird_Eat_crab");
                manager.Food_Field2.transform.gameObject.SetActive(true);
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, manager.str);
                }
                else
                {
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, manager.str);
                }
            }

            manager.ThrowManager.Reset();
            manager.ThrowManager.transform.gameObject.SetActive(false);
            AppManage.Instance.isComplite = false;
        }
    }

    public void Next4()
    {
        if (AppManage.Instance.isComplite)
        {
            manager.ThrowManager.isThrown = false;
            if (AppManage.Instance.Gender == 0)
            {
                manager.CH1_nomal.gameObject.SetActive(false);
                manager.CH2_nomal.gameObject.SetActive(true);
                manager.CH3.gameObject.SetActive(false);
            }
            else
            {
                manager.CH1_nomal.gameObject.SetActive(true);
                manager.CH2_nomal.gameObject.SetActive(false);
                manager.CH3.gameObject.SetActive(false);
            }
            manager.count++;
            //manager.Heart.transform.gameObject.SetActive(true);
            manager.Heart.transform.GetChild(0).gameObject.SetActive(true);
            manager.Heart.transform.GetChild(1).gameObject.SetActive(true);
            string text = Geography_Text_Script2.Instance.scenario[manager.currentStage].text[Geography_Text_Script2.Instance.scenario[manager.currentStage].Num[manager.count]];
            if (manager.count == 12)
            {
                manager.yellowScarf.SetTrigger("Dambi_Eat_fruit");
                manager.str.text = string.Empty;
                manager.TextBox.transform.gameObject.SetActive(false);
                manager.ThrowManager.Reset();
                manager.ThrowManager.transform.gameObject.SetActive(false);
                AppManage.Instance.isComplite = true;
            }

            else if (manager.count == 17)
            {
                manager.ThrowManager.Reset();
                manager.ThrowManager.transform.gameObject.SetActive(false);
                manager.yellowScarf.SetTrigger("Dambi_Eat_bug");
                manager.Food_Field2.transform.gameObject.SetActive(true);
                manager.canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                AppManage.Instance.isComplite = true;
                GameObject.Find("UIManager").SendMessage("CaptureOn");
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
