using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UImanager : MonoBehaviour
{
    public RectTransform Capture;
    public RectTransform Contents;
    public Transform WebCam;
	// Use this for initialization
	void Start ()
    {
        Capture.transform.gameObject.SetActive(false);
        Contents.transform.gameObject.SetActive(true);
        WebCam.transform.gameObject.SetActive(false);
	}
	
    public void CaptureOn()
    {
        Capture.transform.gameObject.SetActive(true);
        Contents.transform.gameObject.SetActive(false);
        WebCam.transform.gameObject.SetActive(true);
        GameObject.Find("CaptureManager").GetComponent<TakeCapture>().TakeShotWithKids(GameObject.Find("CaptureManager").GetComponent<TakeCapture>().Kids, true);
    }

    public void CaptureOff()
    {
        Capture.transform.gameObject.SetActive(false);
        Contents.transform.gameObject.SetActive(true);
        WebCam.transform.gameObject.SetActive(false);
        GameObject.Find("CaptureManager").GetComponent<TakeCapture>().TakeShotWithKids(GameObject.Find("CaptureManager").GetComponent<TakeCapture>().Kids, false);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
