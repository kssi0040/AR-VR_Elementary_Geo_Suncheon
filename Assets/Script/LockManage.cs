using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockManage : MonoBehaviour
{
    public RectTransform Stage1Lock;
    public RectTransform Stage2Lock;
    public RectTransform Stage3Lock;
    public RectTransform Stage4Lock;
    public RectTransform Stage5Lock;
    public RectTransform Stage1Clear;
    public RectTransform Stage2Clear;
    public RectTransform Stage3Clear;
    public RectTransform Stage4Clear;
    public RectTransform Stage5Clear;
    public RectTransform Animal1Get;
    public RectTransform Animal2Get;
    public RectTransform Animal3Get;
    public RectTransform PopupCanvas;
    // Use this for initialization
    void Start ()
    {
        PopupCanvas.transform.gameObject.SetActive(false);
        Stage1Lock.transform.gameObject.SetActive(false);
        if(AppManage.Instance.ClearStage1==1)
        {
            Debug.Log("북촌 해금");
            Stage2Lock.transform.gameObject.SetActive(false);
            Stage1Clear.transform.gameObject.SetActive(true);
        }
        if(AppManage.Instance.ClearStage2==1)
        {
            Debug.Log("나머지 해금");
            Stage2Clear.transform.gameObject.SetActive(true);
            Stage3Lock.transform.gameObject.SetActive(false);
            Stage4Lock.transform.gameObject.SetActive(false);
            Stage5Lock.transform.gameObject.SetActive(false);
            PopupCanvas.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.ClearStage3 == 1)
        {
            Stage3Clear.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.ClearStage4 == 1)
        {
            Stage4Clear.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.ClearStage5 == 1)
        {
            Stage5Clear.transform.gameObject.SetActive(true);
        }
        if(AppManage.Instance.GetAnimal1==1)
        {
            Animal1Get.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.GetAnimal2 == 1)
        {
            Animal2Get.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.GetAnimal3 == 1)
        {
            Animal3Get.transform.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
    
    }

    public void PopupClose()
    {
        PopupCanvas.transform.gameObject.SetActive(false);
    }
}
