using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public List<GameObject> cameras = new List<GameObject>();
    int cameraNum = 0;
	// Use this for initialization
	void Start () {
        foreach(GameObject cam in cameras)
        {
            cam.SetActive(false);
        }
        cameras[cameraNum].SetActive(true);
        UIController.Instance.ActivateCameraButton(SwitchCamera);
	}
	
	void SwitchCamera()
    {
        cameras[cameraNum].SetActive(false);
        if(cameraNum == cameras.Count - 1)
        {
            cameraNum = 0;
        }
        else
        {
            cameraNum++;
        }
        cameras[cameraNum].SetActive(true);
    }
}
