using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour 
{
    public Transform sphere;
    [SerializeField]
    Transform bird;
    
    Rigidbody rb;

    private Swipe swipeControl;


    public bool isThrown;

	// Use this for initialization
	void Start () 
    {
        //sphere.transform.gameObject.SetActive(false);
        swipeControl = GetComponent<Swipe>();
        
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (swipeControl.SwipeUp)
        {
            rb = sphere.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            sphere.transform.gameObject.SetActive(true);
            ThrowObject(sphere);
        }
	}

    public void ThrowObject(Transform _obj)
    {
        
        StartCoroutine(EThrowObject(_obj, bird));

    }

    private float throwAngle = 45.0f;
    private float gravity = 9.8f;

    private IEnumerator EThrowObject(Transform _obj, Transform _target)
    {
        // Move _obj to the position of throwing object + add some offset if needed.
        _obj.position = _obj.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(_obj.position, _target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(3.2f * throwAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(throwAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(throwAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        _obj.rotation = Quaternion.LookRotation(_target.position - _obj.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            _obj.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        isThrown = true;
        sphere.gameObject.SetActive(false);
    }

    public void Reset()
    {
        sphere.gameObject.SetActive(true);
        rb.isKinematic = true;
        sphere.transform.position = new Vector3(0, 0.5f, -8f);
    }
}
