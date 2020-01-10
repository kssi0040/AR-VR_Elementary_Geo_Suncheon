using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class TextManager : MonoBehaviour
{
    public int currentStage;
    public int count = 0;
    public string text;
    
    [SerializeField]
    int tempInt = 0;

    public RectTransform Link;
    public RectTransform Controller;
    public RectTransform LinkCanvas;
    public RectTransform ImgLinkCanvas;
    public RectTransform Typing1Canvas;
    public RectTransform Typing2Canvas;
    public RectTransform OptionCanvas;
    public RectTransform ImgOptionCanvas;

    public Transform sphere;
    public Transform CH1_nomal;
    public Transform CH2_nomal;
    public Transform CH3;
    public Transform Food_1;
    public Transform Food_2;
    public Transform Food_Field1;//들판에 깔린 먹이1
    public Transform Food_Field2;//들판에 깔린 먹이2
    //public Transform CH_Bug;            //풍뎅이

    public Transform Place;

    public Image BackGround;
    public Image PopUp;
    public Image Highlight;
    public Image sphereIcon;

    public RawImage TextBox;
    public RawImage NPCTextBox;

    public Text str;
    public Text PopUpText;
    public Text NPCText;
    
    public Animator CHS;

    public Animator bird;               //눈새 애니
    //public Animator bug;                //풍댕이 에니
    public Animator blackBird;          //흑두루미 애니
    public Animator Heart;
    public Animator yellowScarf;        //노란목도리담비 애니
    public Animator Friend;
    
    public Canvas canvas;

    LinkQuizManager linkQuizManager;
    public Throw ThrowManager;
    

    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {
        linkQuizManager = FindObjectOfType<LinkQuizManager>();
        ThrowManager = FindObjectOfType<Throw>();

        // 퀴즈 캔버스 비활성화(초기에)
        LinkCanvas.transform.gameObject.SetActive(false);
        ImgLinkCanvas.transform.gameObject.SetActive(false);
        Typing1Canvas.transform.gameObject.SetActive(false);
        Typing2Canvas.transform.gameObject.SetActive(false);
        OptionCanvas.transform.gameObject.SetActive(false);
        ImgOptionCanvas.transform.gameObject.SetActive(false);
        // 링크, 팝업, 팝업 텍스트, 북 비활성화(초기에)
        Link.transform.gameObject.SetActive(false);
        PopUp.transform.gameObject.SetActive(false);
        PopUpText.transform.gameObject.SetActive(false);
        
        // 남주, 여주, 선생 비활성화(초기에)
        CH1_nomal.transform.gameObject.SetActive(false);      
        CH2_nomal.transform.gameObject.SetActive(false);
        CH3.transform.gameObject.SetActive(false);
        
        // public 으로 넣어준 것들은 매 scene마다 수동으로 넣어주기 때문에 넣지 않는 scene에서는 사용 자체를 하지 않기 때문에
        // 쓰지 않는 오브젝트는 false를 해줄 필요가 없다.

        // 선생트리거 활성화
        //CHS.SetTrigger("teachAnim");

        // 초기에 플레이어가 나오므로 플레이어 구분 후 에니메이션 활성화
        if(AppManage.Instance.Gender == 0)
        {
            CH2_nomal.transform.gameObject.SetActive(true);
            CHS.SetTrigger("CH2Anim");
        }
        if(AppManage.Instance.Gender == 1)
        {
            CH1_nomal.transform.gameObject.SetActive(true);
            CHS.SetTrigger("CH1Anim");
        }

        // start로 인해 초기에 텍스트창이 뜸(맵에서 선택 후 텍스트에 뿌려짐)
        // XML를 찾아서 text에 넣어주는 과정
        text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
        
        if(text.Contains("###"))    //이름과 텍스트를 같이 뿌려줌
        {
            string temp = text.Replace("###", AppManage.Instance.Name);
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
        }
        else
        {   // 텍스트만 뿌려줌
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
        }
        switch(currentStage)
        {
            case 1:
                NPCText.transform.gameObject.SetActive(false);
                NPCTextBox.transform.gameObject.SetActive(false);
                ThrowManager.GetComponent<Throw>().sphere = Food_1;
                ThrowManager.transform.gameObject.SetActive(false);
                Food_1.transform.gameObject.SetActive(false);
                Food_2.transform.gameObject.SetActive(false);
                Food_Field1.transform.gameObject.SetActive(false);
                Food_Field2.transform.gameObject.SetActive(false);
                bird.transform.gameObject.SetActive(false);
                blackBird.transform.gameObject.SetActive(false);         //흑두루미 오브젝트 비활성화
                Heart.transform.GetChild(0).gameObject.SetActive(false);
                Heart.transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                NPCText.transform.gameObject.SetActive(false);
                NPCTextBox.transform.gameObject.SetActive(false);
                ThrowManager.GetComponent<Throw>().sphere = Food_1;
                ThrowManager.transform.gameObject.SetActive(false);
                sphereIcon.transform.gameObject.SetActive(false);
                sphere.transform.gameObject.SetActive(false);
                Controller.transform.gameObject.SetActive(false);
                Food_1.transform.gameObject.SetActive(false);
                Food_2.transform.gameObject.SetActive(false);
                Food_Field1.transform.gameObject.SetActive(false);
                Food_Field2.transform.gameObject.SetActive(false);
                CH1_nomal.transform.gameObject.SetActive(false);
                CH2_nomal.transform.gameObject.SetActive(false);
                yellowScarf.transform.gameObject.SetActive(false);       //담비 오브젝트 비활성화
                Heart.transform.GetChild(0).gameObject.SetActive(false);
                Heart.transform.GetChild(1).gameObject.SetActive(false);
                bird.transform.gameObject.SetActive(true);
                break;
            case 3:
                Friend.transform.gameObject.SetActive(false);
                CH1_nomal.transform.gameObject.SetActive(false);
                CH2_nomal.transform.gameObject.SetActive(false);
                bird.transform.gameObject.SetActive(false);
                CH3.transform.gameObject.SetActive(true);
                CHS.SetTrigger("teachAnim");
                break;
            case 4:
                Friend.transform.gameObject.SetActive(false);
                //Friend.transform.GetChild(0).gameObject.SetActive(false);
                //Friend.transform.GetChild(1).gameObject.SetActive(false);
                CH1_nomal.transform.gameObject.SetActive(false);
                CH2_nomal.transform.gameObject.SetActive(false);
                bird.transform.gameObject.SetActive(false);
                CH3.transform.gameObject.SetActive(true);
                CHS.SetTrigger("teachAnim");
                break;
            case 5:
                //Friend.transform.GetChild(0).gameObject.SetActive(false);
                Friend.transform.gameObject.SetActive(false);
                CH1_nomal.transform.gameObject.SetActive(false);
                CH2_nomal.transform.gameObject.SetActive(false);
                bird.transform.gameObject.SetActive(false);
                CH3.transform.gameObject.SetActive(true);
                CHS.SetTrigger("teachAnim");
                break;
            default:
                break;
        }
    }
    AnimatorStateInfo animInfo;
    // Update is called once per frame
    void Update()
    {
        switch (currentStage)       //각 스테이지별 업데이트 해줘야하는 상태
        {
            case 1:
                switch (count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                    case 15:
                    case 19:
                        if (ThrowManager.isThrown)
                        {
                            GameObject.Find("EventMaster").SendMessage("Next3");
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch(count)
                {
                    case 11:
                    case 16:
                        if(ThrowManager.isThrown)
                        {
                            GameObject.Find("EventMaster").SendMessage("Next4");
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);       //순천만습지 입구 id값 0번째 
                        break;
                }
                break;
            case 4:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                }
                break;
            case 5:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                }
                break;
        }
    }
    
    // 앞으로 가는 화살표를 누를 시
    public void forwardDown()
    {
        Text tempText;
        
        // 텍스트가 완전히 다 찍히면 if문 성립
        if (AppManage.Instance.isComplite)
        {
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 9:
                            blackBird.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 11:
                            switch(tempInt)
                            {
                                case 0:
                                    if (linkQuizManager.OptionClear == false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        blackBird.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        blackBird.transform.gameObject.SetActive(true);
                                        count++;
                                    }
                                    break;
                            }
                            break;                       
                        case 14:
                            blackBird.transform.gameObject.SetActive(true);
                            ThrowManager.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 15:     
                            break;
                        case 16:
                            Food_Field1.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            Heart.transform.GetChild(0).gameObject.SetActive(false);
                            Heart.transform.GetChild(1).gameObject.SetActive(false);
                            blackBird.SetTrigger("BlackBird_Idle");
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 17:
                            switch (tempInt)
                            {
                                case 1:         //퀴즈 후
                                    if(linkQuizManager.OptionClear == false)
                                    {
                                        str.text = string.Empty;
                                        NPCText.transform.gameObject.SetActive(false);
                                        NPCTextBox.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        blackBird.transform.gameObject.SetActive(false);
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else     // 텍스트해설 띄우기
                                    {
                                        tempInt++;
                                        NPCText.transform.gameObject.SetActive(false);
                                        NPCTextBox.transform.gameObject.SetActive(false);
                                        linkQuizManager.OptionClear = false;
                                        linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        blackBird.transform.gameObject.SetActive(true);
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 18:
                            ThrowManager.transform.gameObject.SetActive(true);
                            ThrowManager.sphere = Food_2;
                            count++;
                            break;
                        case 19:
                            break;
                        case 20:
                            Food_Field2.transform.gameObject.SetActive(false);
                            Heart.transform.GetChild(0).gameObject.SetActive(false);
                            Heart.transform.GetChild(1).gameObject.SetActive(false);
                            blackBird.SetTrigger("BlackBird_Idle");
                            count++;
                            break;
                        case 21:
                            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                            AppManage.Instance.isComplite = true;
                            GameObject.Find("UIManager").SendMessage("CaptureOn");
                            break;
                        case 22 :        //흑두루미와 사진을 찍고 난 후
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 23:
                            AppManage.Instance.EndStage(1);
                            break;
                        default:
                            count++;
                            break;

                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 0:
                            //Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            switch (tempInt)
                            {
                                case 0:
                                    if (linkQuizManager.OptionClear == false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        AppManage.Instance.isComplite = true;
                                        linkQuizManager.GenerateOptionQuiz(1, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            OptionCanvas.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            count++;
                            break;

                        case 2:         //퀴즈 후
                            OptionCanvas.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 3:
                            yellowScarf.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 5:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            yellowScarf.transform.gameObject.SetActive(false);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 6:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            yellowScarf.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 7:
                            switch (tempInt)
                            {
                                case 1:
                                    if (linkQuizManager.OptionClear == false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        yellowScarf.transform.gameObject.SetActive(false);
                                        linkQuizManager.GenerateOptionQuiz(1, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 9:
                            OptionCanvas.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 10:
                            yellowScarf.transform.gameObject.SetActive(true);
                            ThrowManager.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 11:
                            break;
                        case 12:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            yellowScarf.SetTrigger("Dambi_Idle");
                            Heart.transform.GetChild(0).gameObject.SetActive(false);
                            Heart.transform.GetChild(1).gameObject.SetActive(false);
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            count++;
                            break;
                        case 13:        //노란목도리 담비가 먹이를 먹고 난 후
                            switch (tempInt)
                            {
                                case 2:
                                    if (linkQuizManager.OptionClear == false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        yellowScarf.transform.gameObject.SetActive(false);
                                        linkQuizManager.GenerateOptionQuiz(1, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        PopUp.transform.gameObject.SetActive(true);
                                        PopUpText.transform.gameObject.SetActive(true);
                                        PopUp.overrideSprite = Resources.Load<Sprite>("Popup_Png");
                                        GameObject.Find("WebManager").SendMessage("initURL", "http://www.index.go.kr/potal/main/EachDtlPageDetail.do?idx_cd=1171");
                                        Link.transform.gameObject.SetActive(true);
                                        tempText = GameObject.Find("Text").GetComponent<Text>();
                                        tempText.text = "e 나라지표";
                                        CHS.SetTrigger("teachAnim");
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 14:
                            OptionCanvas.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            yellowScarf.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 15:
                            ThrowManager.transform.gameObject.SetActive(true);
                            ThrowManager.sphere = Food_2;
                            count++;
                            break;
                        case 16:
                            break;
                        case 17:
                            str.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 21:
                            SceneManager.LoadScene("Stage2_End");
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            str.text = string.Empty;                        //캐릭말비우기
                            TextBox.transform.gameObject.SetActive(false);  //캐릭터말풍선 끄기
                            PopUp.transform.gameObject.SetActive(true);     //팝업 키기
                            PopUpText.transform.gameObject.SetActive(true);
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.suncheonbay.go.kr/?c=2/26/34/124");   //순천만습지 팝업 주소
                            Link.transform.gameObject.SetActive(false);      //링크 활성화
                            //tempText = GameObject.Find("Text").GetComponent<Text>();    // 링크를 들어가기 위한 버튼
                            //tempText.text = "순천만습지 홈페이지";                      // 링크 버튼 이름
                            count++;
                            break;
                        case 1:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            count++;
                            break;
                        case 2:
                            if (linkQuizManager.OptionClear == false)
                            {
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                AppManage.Instance.isComplite = true;
                                linkQuizManager.GenerateOptionQuiz(2, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH1_nomal.gameObject.SetActive(false);
                                    CH2_nomal.gameObject.SetActive(true);
                                    CH3.gameObject.SetActive(false);
                                    bird.gameObject.SetActive(false);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.gameObject.SetActive(true);
                                    CH2_nomal.gameObject.SetActive(false);
                                    CH3.gameObject.SetActive(false);
                                    bird.gameObject.SetActive(false);
                                    CHS.SetTrigger("CH1Anim");
                                }
                            }
                            else
                            {
                                tempInt++;
                                AppManage.Instance.isComplite = true;
                                linkQuizManager.GenerateOptionQuiz(2, tempInt);
                                linkQuizManager.OptionClear = false;
                            }
                            if (tempInt == 2)
                            {
                                OptionCanvas.transform.gameObject.SetActive(false);
                                tempInt = 0;
                                TextBox.transform.gameObject.SetActive(true);
                                str.transform.gameObject.SetActive(true);
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(true);
                                bird.gameObject.SetActive(false);
                                CHS.SetTrigger("teachAnim");
                                count++;
                            }
                            break;
                        case 3:
                            Friend.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 4:
                            AppManage.Instance.EndStage(3);
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUp.overrideSprite = Resources.Load<Sprite>("Popup_Png");
                            GameObject.Find("WebManager").SendMessage("initURL", "http://garden.sc.go.kr/?r=home");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "순천만 국가정원 홈페이지";
                            TextBox.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 1:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            count++;
                            break;
                        case 2:
                            switch (tempInt)
                            {
                                case 0:         //퀴즈 후
                                    if(linkQuizManager.OptionClear == false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        linkQuizManager.GenerateOptionQuiz(3, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else     // 텍스트해설 띄우기
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        linkQuizManager.GenerateOptionQuiz(3, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        CH1_nomal.gameObject.SetActive(false);
                                        CH2_nomal.gameObject.SetActive(false);
                                        CH3.gameObject.SetActive(true);
                                        bird.gameObject.SetActive(false);
                                        CHS.SetTrigger("teachAnim");
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 3:
                            switch (tempInt)
                            {
                                case 1:
                                    if (linkQuizManager.OptionClear == false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        linkQuizManager.GenerateOptionQuiz(3, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            bird.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        linkQuizManager.GenerateOptionQuiz(3, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        CH1_nomal.gameObject.SetActive(false);
                                        CH2_nomal.gameObject.SetActive(false);
                                        CH3.gameObject.SetActive(true);
                                        bird.gameObject.SetActive(false);
                                        CHS.SetTrigger("teachAnim");
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 4:
                            //Friend.transform.GetChild(0).gameObject.SetActive(true);
                            //Friend.transform.GetChild(1).gameObject.SetActive(true);
                            Friend.transform.gameObject.SetActive(true);
                            Friend.SetTrigger("SuDal_Dark");
                            count++;
                            break;
                        case 5:
                            AppManage.Instance.EndStage(4);
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 5:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUp.overrideSprite = Resources.Load<Sprite>("Popup_Png");
                            GameObject.Find("WebManager").SendMessage("initURL", "http://www.suncheonbay.go.kr/");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "순천만 자연 생태관";
                            TextBox.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 1:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            count++;
                            break;
                        case 2:
                            if (linkQuizManager.OptionClear == false)
                            {
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                AppManage.Instance.isComplite = true;
                                linkQuizManager.GenerateOptionQuiz(4, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH1_nomal.gameObject.SetActive(false);
                                    CH2_nomal.gameObject.SetActive(true);
                                    CH3.gameObject.SetActive(false);
                                    bird.gameObject.SetActive(false);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.gameObject.SetActive(true);
                                    CH2_nomal.gameObject.SetActive(false);
                                    CH3.gameObject.SetActive(false);
                                    bird.gameObject.SetActive(false);
                                    CHS.SetTrigger("CH1Anim");
                                }
                            }
                            else
                            {
                                tempInt++;
                                str.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                AppManage.Instance.isComplite = true;
                                linkQuizManager.GenerateOptionQuiz(4, tempInt);
                                linkQuizManager.OptionClear = false;
                            }
                            if (tempInt == 2)
                            {
                                OptionCanvas.transform.gameObject.SetActive(false);
                                tempInt = 0;
                                TextBox.transform.gameObject.SetActive(true);
                                str.transform.gameObject.SetActive(true);
                                CHS.SetTrigger("teachAnim");
                                count++;
                            }
                            break;
                        case 3:
                            //Friend.transform.GetChild(0).gameObject.SetActive(true);
                            Friend.transform.gameObject.SetActive(true);
                            Friend.SetTrigger("Crab_Dark");
                            count++;
                            break;
                        case 4:
                            AppManage.Instance.EndStage(5);
                            break;
                        default:
                            OptionCanvas.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            count++;
                            break;
                    }
                    break;
                default:
                    break;
            }
            // 텍스트문을 뿌려주는 switch
            switch(currentStage)
            {
                case 1:
                    switch(count)
                    {
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 2:
                    switch(count)
                    {
                        case 6:
                        case 14:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 12:
                        case 18:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 1:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }

                            break;
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }

                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 1:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 1:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
            }

            // 에니메이션이 출력되는 오브젝트들 활성화
            switch(currentStage)
            {
                case 1:
                    switch(count)
                    {
                        case 2:
                        case 6:
                        case 8:
                        case 11:
                        case 13:
                        case 15:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);                                
                                bird.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);                                
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);                                
                                bird.transform.gameObject.SetActive(false);
                            }
                            break;
                        case 17:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 2:
                    switch(count)
                    {
                        case 0:
                        case 13:
                            break;
                        case 2:
                        case 4:
                        case 7:
                        case 9:
                        case 11:
                        case 12:
                        case 16:
                        case 17:
                        case 18:
                        case 20:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                                bird.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                                bird.transform.gameObject.SetActive(false);
                            }
                            break;
                        case 6:
                        case 10:
                        case 14:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            bird.transform.gameObject.SetActive(false);
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 3:         
                    switch (count)          // 순천만습지입구 에니메이션이 출력되는 오브젝트 활성화
                    {

                        case 1:
                        case 3:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            bird.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                        case 2:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            break;
                    }
                    break;

                case 4:
                    switch (count)          
                    {
                        case 0:
                        case 1:
                        case 4:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            bird.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                        case 2:
                        case 3:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            break;
                    }
                    break;

                case 5:
                    switch (count)
                    {
                        case 1:
                        case 3:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            bird.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                        case 2:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            break;
                    }
                    break;
                default:
                    break;
            }
            // 플레이어 에니메이션을 키기 위한 if문(anim 배열에 *인 위치가 플레이어 에니메이션 부분이므로)
            if (Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]] == "*")
            {
                if (AppManage.Instance.Gender == 0)
                {
                    CHS.SetTrigger("CH2Anim");
                }
                else
                {
                    CHS.SetTrigger("CH1Anim");
                }
            }
            else        //npc 에니메이션을 키기 위한 else문
            {
                if (currentStage == 1)
                {
                    if (count == 16)
                    {
                        blackBird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                    else
                    {
                        CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                        bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if (currentStage == 2)
                {
                    if (count == 12)
                    {
                        yellowScarf.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }

                    else if (count == 18)
                    {
                        yellowScarf.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                    else
                    {
                        CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                        bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if (currentStage == 3)
                {
                    if (count != 2)
                    {
                        CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                        bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if (currentStage == 4)
                {
                    if (count != 2 && count != 3)
                    {
                        CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                        bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if (currentStage == 5)
                {
                    if (count != 2)
                    {
                        CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                        bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else
                {
                    bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                }
            }
            AppManage.Instance.isComplite = false;
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
    }
    // 뒤로 가기 화살표를 누를 시
    public void BeforeDown()
    {
        Text tempText;
        if (AppManage.Instance.isComplite)
        {
            if (count > 0)
            {
                count--;
            }
            else
            {
                SceneManager.LoadScene("SelectMap");
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(false);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            break;
                        case 1:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            break;
                        case 9:
                            blackBird.transform.gameObject.SetActive(false);
                            break;
                        case 10:
                            tempInt = 0;
                            linkQuizManager.OptionClear = false;
                            blackBird.transform.gameObject.SetActive(true);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            break;
                        case 11:
                            tempInt = 0;
                            linkQuizManager.OptionClear = false;
                            blackBird.transform.gameObject.SetActive(true);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            break;
                        case 13:        
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            break;
                        case 15:
                            ThrowManager.GetComponent<Throw>().sphere = Food_1;
                            ThrowManager.transform.gameObject.SetActive(true);
                            Food_Field1.transform.gameObject.SetActive(false);
                            Heart.transform.GetChild(0).gameObject.SetActive(false);
                            Heart.transform.GetChild(1).gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            blackBird.SetTrigger("BlackBird_Idle");
                            break;
                        case 16:
                            tempInt = 1;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            Food_Field1.transform.gameObject.SetActive(true);
                            Heart.transform.GetChild(0).gameObject.SetActive(true);
                            Heart.transform.GetChild(1).gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            blackBird.transform.gameObject.SetActive(true);
                            blackBird.SetTrigger("BlackBird_Eat_seed");
                            break;
                        case 17:
                            tempInt = 1;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            break;
                        case 18:
                            ThrowManager.transform.gameObject.SetActive(false);
                            break;
                        case 19:
                            ThrowManager.sphere = Food_2;
                            ThrowManager.transform.gameObject.SetActive(true);
                            Food_Field2.transform.gameObject.SetActive(false);
                            Heart.transform.gameObject.SetActive(false);
                            blackBird.SetTrigger("BlackBird_Idle");
                            break;
                        case 20:
                            Food_Field2.transform.gameObject.SetActive(true);
                            Heart.transform.gameObject.SetActive(true);
                            blackBird.SetTrigger("BlackBird_Eat_crab");
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 0:
                            tempInt = 0;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            break;
                        case 3:         //퀴즈 후
                            yellowScarf.transform.gameObject.SetActive(false);
                            break;
                        case 5:
                            yellowScarf.transform.gameObject.SetActive(true);
                            break;
                        case 6:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            tempInt = 1;
                            yellowScarf.transform.gameObject.SetActive(false);
                            break;
                        case 7:
                            yellowScarf.transform.gameObject.SetActive(true);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            tempInt = 1;
                            break;
                        case 8:
                            tempInt = 1;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            break;
                        case 11:
                            ThrowManager.GetComponent<Throw>().sphere = Food_1;
                            ThrowManager.transform.gameObject.SetActive(true);
                            yellowScarf.SetTrigger("Dambi_Idle");
                            Heart.transform.GetChild(0).gameObject.SetActive(false);
                            Heart.transform.GetChild(1).gameObject.SetActive(false);
                            break;
                        case 12:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            tempInt = 2;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            yellowScarf.transform.gameObject.SetActive(true);
                            yellowScarf.SetTrigger("Dambi_Eat_fruit");
                            Heart.transform.GetChild(0).gameObject.SetActive(true);
                            Heart.transform.GetChild(1).gameObject.SetActive(true);
                            break;
                        case 13:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            tempInt = 2;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            yellowScarf.transform.gameObject.SetActive(true);
                            break;
                        case 14:
                            yellowScarf.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            PopUp.overrideSprite = Resources.Load<Sprite>("Popup_Png");
                            GameObject.Find("WebManager").SendMessage("initURL", "http://www.index.go.kr/potal/main/EachDtlPageDetail.do?idx_cd=1171");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "e 나라지표";
                            break;
                        case 15:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            Link.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            break;
                        case 17:
                            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                            AppManage.Instance.isComplite = true;
                            GameObject.Find("UIManager").SendMessage("CaptureOn");
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(false);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            Link.transform.gameObject.SetActive(false);
                            break;
                        case 1:
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);  //캐릭터말풍선 끄기
                            OptionCanvas.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);     //팝업 키기
                            PopUpText.transform.gameObject.SetActive(true);
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.suncheonbay.go.kr/?c=2/26/34/124");   //순천만습지 팝업 주소
                            Link.transform.gameObject.SetActive(false);      //링크 활성화
                            //tempText = GameObject.Find("Text").GetComponent<Text>();    // 링크를 들어가기 위한 버튼
                            //tempText.text = "순천만습지 형성과정";                      // 링크 버튼 이름
                            tempInt = 0;
                            linkQuizManager.OptionClear = false;
                            break;
                        case 2:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            tempInt = 0;
                            break;
                        case 3:
                            Friend.transform.gameObject.SetActive(false);
                            break;
                        default:
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(false);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            break;
                        case 1:
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUp.overrideSprite = Resources.Load<Sprite>("Popup_Png");
                            str.transform.gameObject.SetActive(false);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            GameObject.Find("WebManager").SendMessage("initURL", "http://garden.sc.go.kr/?r=home");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "순천만 국가정원 홈페이지";
                            TextBox.transform.gameObject.SetActive(false);
                            tempInt = 0;
                            break;
                        case 2:
                            tempInt = 0;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            break;
                        case 3:
                            tempInt = 1;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            break;
                        case 4:
                            tempInt = 0;
                            linkQuizManager.OptionClear = false;
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            Friend.transform.gameObject.SetActive(false);
                            break;
                        default:
                            linkQuizManager.OptionClear = false;
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(false);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            Link.transform.gameObject.SetActive(false);
                            break;

                        case 1:
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(false);
                            PopUp.overrideSprite = Resources.Load<Sprite>("Popup_Png");
                            GameObject.Find("WebManager").SendMessage("initURL", "http://www.suncheonbay.go.kr/");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "순천만 자연 생태관";
                            TextBox.transform.gameObject.SetActive(false);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            tempInt = 0;               
                            break;
                        case 2:
                            tempInt = 0;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;

                            break;
                        case 3:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            Friend.transform.gameObject.SetActive(false);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 16:        //흑두루미 텍스트
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 6:
                        case 14:        //14
                            str.transform.gameObject.SetActive(false);      //캐릭터 string 비활성화
                            TextBox.transform.gameObject.SetActive(false);  //캐릭터말풍선 끄기
                            PopUp.transform.gameObject.SetActive(true);     //팝업 키기
                            PopUpText.transform.gameObject.SetActive(true);
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 17:
                            break;
                        case 11:        //11,17
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                        case 12:        //12,18
                            break;
                        case 13:        //13,19
                        case 19:
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);

                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;

                        default:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 1:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }

                            break;
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 1:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            PopUpText.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 1:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 0:
                        case 2:
                        case 6:
                        case 8:
                        case 11:
                        case 13:
                        case 15:
                        case 16:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);                                
                                bird.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                                bird.transform.gameObject.SetActive(false);
                            }
                            break;
                        case 10:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.transform.gameObject.SetActive(true);
                            break;

                        //case 16:
                        //    CH1_nomal.gameObject.SetActive(false);
                        //    CH2_nomal.gameObject.SetActive(false);
                        //    CH3.gameObject.SetActive(false);
                        //    bird.transform.gameObject.SetActive(true);
                        //    break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);                        
                            CH3.gameObject.SetActive(false);
                            bird.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 2:
                        case 4:
                        case 7:
                        case 9:
                        case 11:
                        case 12:
                        case 16:
                        case 17:
                        case 18:
                        case 20:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                                bird.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                                bird.transform.gameObject.SetActive(false);
                            }
                            break;
                        case 6:
                        case 10:
                        case 14:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            bird.transform.gameObject.SetActive(false);                           
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 3:
                    switch (count)          // 순천만습지입구 에니메이션이 출력되는 오브젝트 활성화
                    {
                        case 0:
                        case 1:
                        case 3:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            bird.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;

                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 0:
                        case 1:
                        case 3:
                        case 4:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            bird.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;

                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 0:
                        case 1:
                        case 3:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            bird.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;

                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            bird.gameObject.SetActive(true);
                            break;
                    }
                    break;

                default:
                    break;
            }
            if (Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]] == "*")
            {
                if (AppManage.Instance.Gender == 0)
                {
                    CHS.SetTrigger("CH2Anim");
                }
                else
                {
                    CHS.SetTrigger("CH1Anim");
                }
            }
            else if (Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]] == "teacherAnim")
            {
                CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                CHS.SetTrigger("teachAnim");

            }
            else        //npc 에니메이션을 키기 위한 else문
            {
                if (currentStage == 1)
                {
                    if (count == 16)
                    {
                        blackBird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                    else
                    {
                        bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if (currentStage == 2)
                {
                    if (count == 12)
                    {
                        yellowScarf.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);

                        if (AppManage.Instance.Gender == 0)
                        {
                            CHS.SetTrigger("CH2Anim");
                        }
                        else
                        {
                            CHS.SetTrigger("CH1Anim");
                        }
                    }

                    else if (count == 18)
                    {
                        yellowScarf.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                    else
                    {
                        bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else
                {
                    bird.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                }
            }
            
            AppManage.Instance.isComplite= false;
            if(currentStage==2)
            {
                if(count ==12)
                {
                    AppManage.Instance.isComplite = true;
                }
                else if(count==17)
                {
                    AppManage.Instance.isComplite=true;
                }
            }
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
    }
    // 사진 찍기 기능에서 나가기 버튼
    public void ExitCapture()
    {
        if (AppManage.Instance.isComplite)
        {
            if (currentStage == 1)
            {
                GameObject.Find("UIManager").SendMessage("CaptureOff");
                AppManage.Instance.isComplite = false;
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                count++;
                if (AppManage.Instance.Gender == 0)
                {
                    CH1_nomal.gameObject.SetActive(false);
                    CH2_nomal.gameObject.SetActive(true);
                    CH3.gameObject.SetActive(false);
                }
                else
                {
                    CH1_nomal.gameObject.SetActive(true);
                    CH2_nomal.gameObject.SetActive(false);
                    CH3.gameObject.SetActive(false);
                }
                text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                }
                else
                {
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                }

                if (Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]] == "*")
                {
                    if (AppManage.Instance.Gender == 0)
                    {
                        CHS.SetTrigger("CH2Anim");
                    }
                    else
                    {
                        CHS.SetTrigger("CH1Anim");
                    }
                }
                else
                {
                    CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                }
            }
            else if (currentStage == 2)
            {
                GameObject.Find("UIManager").SendMessage("CaptureOff");
                AppManage.Instance.isComplite = false;
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                yellowScarf.SetTrigger("Dambi_Idle");
                Heart.transform.gameObject.SetActive(false);
                count++;
                if (AppManage.Instance.Gender == 0)
                {
                    CH1_nomal.gameObject.SetActive(false);
                    CH2_nomal.gameObject.SetActive(true);
                    CH3.gameObject.SetActive(false);
                }
                else
                {
                    CH1_nomal.gameObject.SetActive(true);
                    CH2_nomal.gameObject.SetActive(false);
                    CH3.gameObject.SetActive(false);
                }
                text = Geography_Text_Script2.Instance.scenario[currentStage].text[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]];
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                }
                else
                {
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                }

                if (Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]] == "*")
                {
                    if (AppManage.Instance.Gender == 0)
                    {
                        CHS.SetTrigger("CH2Anim");
                    }
                    else
                    {
                        CHS.SetTrigger("CH1Anim");
                    }
                }
                else
                {
                    CHS.SetTrigger(Geography_Text_Script2.Instance.scenario[currentStage].anim[Geography_Text_Script2.Instance.scenario[currentStage].Num[count]]);
                }
            }
        }
    }    
}
