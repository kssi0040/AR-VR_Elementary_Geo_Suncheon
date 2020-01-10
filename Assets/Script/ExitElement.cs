using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
public class ExitElement : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate { AppManage.Instance.OnExitPopup(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
