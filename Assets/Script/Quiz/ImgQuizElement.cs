using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ImgQuizElement : MonoBehaviour
{
    public Button quiBut;
    public RectTransform lrPos;
    UILineConnector m_UILineConnector;

    // Use this for initialization
    void Start ()
    {
        m_UILineConnector = FindObjectOfType<UILineConnector>();
        quiBut.onClick.AddListener(delegate { m_UILineConnector.ImgQuesButtonCallBack(quiBut,lrPos); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
