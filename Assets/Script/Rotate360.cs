using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour {

    GameObject GyroControl;
    bool gyroEnabled;
    Quaternion rot;
    Gyroscope gyro;
   
	// Use this for initialization
	void Start () 
    {
        GyroControl = new GameObject("Gyro Control");
        GyroControl.transform.position = transform.position;
        transform.SetParent(GyroControl.transform);
        gyroEnabled = EnableGyro();
	}

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            GyroControl.transform.rotation = Quaternion.Euler(90f, -90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }
    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }
}
