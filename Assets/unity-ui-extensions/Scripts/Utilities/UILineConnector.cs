namespace UnityEngine.UI.Extensions
{
    using System.Collections.Generic;
    using System.Collections;

    [AddComponentMenu("UI/Extensions/UI Line Connector")]
    [ExecuteInEditMode]
    public class UILineConnector : MonoBehaviour
    {
        //Quiz_XML_Reader quiz_XML_Reader;
        Society_Quiz2 society_Quiz2;

        // The elements between which line segments should be drawn
        private RectTransform[] transforms;
        private RectTransform[] lrTransforms;

        public bool isDrawn;
        private bool isQuesClicked;

        public UILineRenderer[] m_LineRenderer;
        [SerializeField]
        private Text notifyMsg;

        public int index = -1;

        Button quizBut;
        Button ansBut;

        int ansIndex;

        public bool isInitialized;

        void Awake()
        {
            //quiz_XML_Reader = FindObjectOfType<Quiz_XML_Reader>();
            society_Quiz2 = FindObjectOfType<Society_Quiz2>();
        }

        void Start()
        {
            transforms = new RectTransform[2];
            lrTransforms = new RectTransform[2];
            notifyMsg.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

            Debug.Log("UILineConnector Update" + " index: " + index + " m_LineRenderer.Length: " + m_LineRenderer.Length
                + " isDrawn: " + isDrawn);

            if (isInitialized) //if (m_LineRenderer.Length == 0)
            {
                //m_LineRenderer = new UILineRenderer[transform.childCount];

                //         for (int i = 0; i < m_LineRenderer.Length; i++)
                //{
                //    m_LineRenderer[i] = transform.GetChild(i).GetComponent<UILineRenderer>();

                //}
                notifyMsg.text = string.Empty;
                isInitialized = false;         
            }

            if (!isDrawn || index >= m_LineRenderer.Length)
                return;

            /*
            if (transforms == null || transforms.Length < 1)
            {
                return;
            }
            */

            RectTransform rt = GetComponent<RectTransform>();
            RectTransform canvas = GetComponentInParent<RectTransform>().GetParentCanvas().GetComponent<RectTransform>();

            // Get the pivot points
            Vector2 thisPivot = rt.pivot;
            Vector2 canvasPivot = canvas.pivot;

            // Set up some arrays of coordinates in various reference systems
            Vector3[] worldSpaces = new Vector3[transforms.Length];
            Vector3[] canvasSpaces = new Vector3[transforms.Length];
            Vector2[] points = new Vector2[transforms.Length];

            // First, convert the pivot to worldspace
            for (int i = 0; i < lrTransforms.Length; i++)
            {
                worldSpaces[i] = lrTransforms[i].TransformPoint(thisPivot);
            }

            // Then, convert to canvas space
            for (int i = 0; i < lrTransforms.Length; i++)
            {
                canvasSpaces[i] = canvas.InverseTransformPoint(worldSpaces[i]);
            }

            // Calculate delta from the canvas pivot point
            for (int i = 0; i < lrTransforms.Length; i++)
            {
                points[i] = new Vector2(canvasSpaces[i].x, canvasSpaces[i].y);
            }

            Debug.Log("index: " + index);

            // And assign the converted points to the line renderer
            m_LineRenderer[index].Points = points;
            m_LineRenderer[index].RelativeSize = false;

            
            
        }
        /// <summary>
        /// Quiz 앞 버틈 눌렀을 때
        /// </summary>
        /// <param name="but"></param>
        public void QuesButtonCallBack(Button but,RectTransform _lrPos)
        {
            if (isQuesClicked == false)
            {
                quizBut = but;
                transforms[0] = quizBut.GetComponent<RectTransform>();

                lrTransforms[0] = _lrPos;
                lrTransforms[0].position = new Vector2(lrTransforms[0].position.x + 45, lrTransforms[0].position.y);

                isQuesClicked = true;
                isDrawn = false;

                quizBut.interactable = false;

                //ansIndex = int.Parse(quizBut.transform.GetChild(0).GetComponent<Text>().text);
                ansIndex = int.Parse(quizBut.transform.GetChild(0).GetComponent<Text>().transform.name);
            }
        }
        /// <summary>
        /// Quiz 뒤 버튼 눌렸을 때
        /// </summary>
        /// <param name="but"></param>
        public void AnsButtonCallBack(Button but, RectTransform _lrPos)
        {
            if (!isQuesClicked)
                return;

            ansBut = but;

            //if (ansBut.transform.GetChild(0).GetComponent<Text>().text == society_Quiz2.LinkQuizDict[ansIndex].Answer[0])
            if (ansBut.transform.GetChild(0).GetComponent<Text>().transform.name == society_Quiz2.LinkQuizDict[ansIndex].Answer[0])
            {
                transforms[1] = ansBut.GetComponent<RectTransform>();

                lrTransforms[1] = _lrPos;
                lrTransforms[1].position = new Vector2(lrTransforms[1].position.x - 45, lrTransforms[1].position.y);

                index++;

                isDrawn = true;
                isQuesClicked = false;

                ansBut.interactable = false;

                StartCoroutine(INotifyMsg("o", Color.green));

                if (index>0&&index == m_LineRenderer.Length-1)
                {
                    GameObject.Find("QuizManager").GetComponent<LinkQuizManager>().LinkClear=true;
                    
                }
            }
            else
            {
                isQuesClicked = false;
                quizBut.interactable = true;

                StartCoroutine(INotifyMsg("x", Color.red));
            }
        }

        public void ImgQuesButtonCallBack(Button but, RectTransform _lrPos)
        {
            if (isQuesClicked == false)
            {
                quizBut = but;
                transforms[0] = quizBut.GetComponent<RectTransform>();

                lrTransforms[0] = _lrPos;
                lrTransforms[0].position = new Vector2(lrTransforms[0].position.x + 45, lrTransforms[0].position.y);

                isQuesClicked = true;
                isDrawn = false;

                quizBut.interactable = false;

                //ansIndex = int.Parse(quizBut.transform.GetChild(0).GetComponent<Text>().text);
                ansIndex = int.Parse(quizBut.transform.GetChild(0).transform.name);
            }
        }
        /// <summary>
        /// Quiz 뒤 버튼 눌렸을 때
        /// </summary>
        /// <param name="but"></param>
        public void ImgAnsButtonCallBack(Button but, RectTransform _lrPos)
        {
            if (!isQuesClicked)
                return;

            ansBut = but;

            //if (ansBut.transform.GetChild(0).GetComponent<Text>().text == society_Quiz2.LinkQuizDict[ansIndex].Answer[0])
            if (ansBut.transform.GetChild(0).transform.name == society_Quiz2.ImgLinkQuizDict[ansIndex].Answer[0])
            {
                transforms[1] = ansBut.GetComponent<RectTransform>();

                lrTransforms[1] = _lrPos;
                lrTransforms[1].position = new Vector2(lrTransforms[1].position.x - 45, lrTransforms[1].position.y);

                index++;

                isDrawn = true;
                isQuesClicked = false;

                ansBut.interactable = false;

                StartCoroutine(INotifyMsg("o", Color.green));

                if (index > 0 && index == m_LineRenderer.Length - 1)
                {
                    GameObject.Find("QuizManager").GetComponent<LinkQuizManager>().ImgLinkClear = true;

                }
            }
            else
            {
                isQuesClicked = false;
                quizBut.interactable = true;

                StartCoroutine(INotifyMsg("x", Color.red));
            }
        }

        IEnumerator INotifyMsg(string msg, Color color)
        {
            notifyMsg.text = msg;
            notifyMsg.color = color;

            notifyMsg.enabled = true;
            yield return new WaitForSeconds(1f);
            notifyMsg.enabled = false;
        }

    }

}