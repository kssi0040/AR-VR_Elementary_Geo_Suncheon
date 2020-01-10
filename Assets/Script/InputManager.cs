using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public InputField ID;
    public InputField PW;
    public Toggle Man;
    public Toggle Woman;


    private void Awake()
    {
        ID = GameObject.Find("ID").GetComponent<InputField>();
        PW = GameObject.Find("PW").GetComponent<InputField>();
        Man = GameObject.Find("Man").GetComponent<Toggle>();
        Woman = GameObject.Find("Woman").GetComponent<Toggle>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initInputData()
    {
        ID.text = AppManage.Instance.Name;
        Debug.Log(AppManage.Instance.Gender.ToString() + ">!");
        if (AppManage.Instance.Gender > 0)
        {
            Woman.isOn = true;
            Man.isOn = false;
        }
        else
        {
            Man.isOn = true;
            Woman.isOn = false;
        }
    }
}
