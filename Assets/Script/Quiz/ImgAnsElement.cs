using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ImgAnsElement : MonoBehaviour
{
    public Button ansBut;
    public RectTransform lrPos;
    UILineConnector m_UILineConnector;

    // Use this for initialization
    void Start ()
    {
        m_UILineConnector = FindObjectOfType<UILineConnector>();
        ansBut.onClick.AddListener(delegate { m_UILineConnector.ImgAnsButtonCallBack(ansBut,lrPos); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
