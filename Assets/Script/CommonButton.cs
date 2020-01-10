using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CommonButton : MonoBehaviour
{
    public GameObject Alert;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HomeButtonDown()
    {
        SceneManager.LoadScene("Homepage");
    }

    public void StartButtonDown()
    {
        //if (System.IO.File.Exists(AppManage.Instance.filePath) == false)
        //{
        //    AppManage.Instance.Write_information();
        //}
        if (PlayerPrefs.HasKey("Name") == false)
        {
            AppManage.Instance.Write_information();
        }
        if (Geography_Text_Script2.Instance.readCompleted == true && Geography_Quiz_Script1.Instance.readCompleted == true)
        {
            AppManage.Instance.LoadInformation();
            SceneManager.LoadScene("Prologue");
        }
    }

    public void Stage1ButtonDown()
    {
        SceneManager.LoadScene("Stage1+360");
    }

    public void Stage2ButtonDown()
    {
        if (AppManage.Instance.ClearStage1 > 0)
        {
            SceneManager.LoadScene("Stage2+360");
        }
    }

    public void Stage3ButtonDown()
    {
        if (AppManage.Instance.ClearStage2 > 0)
        {
            SceneManager.LoadScene("Stage3");
        }
    }

    public void Stage4ButtonDown()
    {
        if (AppManage.Instance.ClearStage2 > 0)
        {
            SceneManager.LoadScene("Stage4");
        }
    }

    public void Stage5ButtonDown()
    {
        if (AppManage.Instance.ClearStage2 > 0)
        {
            SceneManager.LoadScene("Stage5");
        }
    }
    public void goingMap1()
    {
        if (AppManage.Instance.isComplite)
        {
            AppManage.Instance.EndStage(1);
        }
    }

    public void EndStage2()
    {
        if (AppManage.Instance.isComplite)
        {
            AppManage.Instance.EndStage(2);
        }
    }

    public void goingSelectMap()
    {
        AppManage.Instance.isClicked = false;
        AppManage.Instance.isComplite = false;
        SceneManager.LoadScene("SelectMap");
    }

    public void OkButtonDown()
    {
        if (Alert != null)
        {
            if (Alert.activeSelf != false)
            {
                Alert.SetActive(false);
            }
        }
    }
}
