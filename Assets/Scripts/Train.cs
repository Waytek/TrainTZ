using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour {
    public Rigidbody teleportPoint;
    public List<Transform> wayPoints = new List<Transform>();
    public float maxSpeed = 10;
    public float acceleratonUp = 0.05f;
    public float acceleratonDown = 0.3f;
    public float angularSpeed = 5;
    public bool isStoped = false;
    public System.Action Stop;
    public System.Action Go;

    // Use this for initialization
    void Start () {

    }

    public float speed = 0;
    // Update is called once per frame
    public int i = 0;
    public float t_acceleratin = 0;
    void FixedUpdate()
    {
        float needSpeed = Mathf.Lerp(0, maxSpeed, UIController.Instance.GetSliderValue());
        if (needSpeed > speed)
            speed = Mathf.Lerp(speed, needSpeed, acceleratonUp);
        else
        {
            speed = Mathf.Lerp(speed, needSpeed, acceleratonDown);            
        }
        if (Vector3.Distance(transform.position, wayPoints[i].position) < 5)
        {
            if (i < wayPoints.Count - 1)
                i++;
            else
                i = 0;
        }
        if (speed < 0.1f)
        {
            if (isStoped == false)
            {
                speed = 0;
                isStoped = true;
                if(Stop !=null)
                    Stop.Invoke();
            }
        }
        if (speed > 0.1f)
        {
            if (isStoped == true)
            {
                isStoped = false;
                if (Go != null)
                    Go.Invoke();
            }
        }
        Vector3 direction = (wayPoints[i].position - transform.position).normalized;
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime*speed*2);
        transform.rotation = lookAt;
        
              
        transform.position += transform.forward*speed* Time.deltaTime;
	}
}
