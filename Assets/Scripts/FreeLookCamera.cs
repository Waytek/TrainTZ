using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FreeLookCamera : MonoBehaviour
{

    public Transform target;

    public float targetHeight = 2.0f;
    public float distance = 2.8f;
    public float maxDistance = 10;
    public float minDistance = 0.5f;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -40;
    public float yMaxLimit = 80;
    public float zoomRate = 20;
    public float rotationDampening = 3.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    public bool isTalking = false;
    



    public void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
    float previosLenght = 0;
    bool uiInput = false;
    bool clicked = false;
    public void StartTouch()
    {
        uiInput = false;
        //bool clicked = false;
    }
    public void EndTouch()
    {
        uiInput = true;
    }
    public void LateUpdate()
    {
        if (!target)
            return;
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                uiInput = true;                
            }
            else
            {
                uiInput = false;
            }

        if (!uiInput)
        {
            if (Input.touchCount == 1)
            {
                x += Input.GetTouch(0).deltaPosition.x * xSpeed * 0.02f;
                y -= Input.GetTouch(0).deltaPosition.y * ySpeed * 0.02f;
            }

            float length = 0;
            if (Input.touchCount == 2)
            {

                length = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;
                if (previosLenght - length > 0)
                    distance += ((previosLenght + length) * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
                if (previosLenght - length < 0)
                    distance -= ((previosLenght + length) * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
                previosLenght = length;
            }
        }
//#if UNITY_EDITOR
        
//        if(!clicked)
//            if (Input.GetMouseButton(0))
//            {
//                if (EventSystem.current.IsPointerOverGameObject())
//                {
//                    uiInput = true;
//                }
//                else
//                {
//                    uiInput = false;
//                }
//                clicked = true;
//            }
//        if (clicked)
//            if (Input.GetMouseButtonUp(0))
//                clicked = false;
//        if (!uiInput)
//        {
//            if (Input.GetMouseButton(0) || (Input.GetMouseButton(0)))
//            {
//                x += Input.GetAxis("Mouse X") * xSpeed * 10 * 0.02f;
//                y -= Input.GetAxis("Mouse Y") * ySpeed * 10 * 0.02f;
//            }

//        }
//        distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
//#endif
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

        Vector3 position = target.position - (rotation * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));
        transform.position = position;

        RaycastHit hit;
        Vector3 trueTargetPosition = target.transform.position - new Vector3(0, -targetHeight, 0);
        if (Physics.Linecast(trueTargetPosition, transform.position, out hit))
        {
            float tempDistance = Vector3.Distance(trueTargetPosition, hit.point) - 0.28f;
            position = target.position - (rotation * Vector3.forward * tempDistance + new Vector3(0, -targetHeight, 0));
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);

    }
}