using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCollider : MonoBehaviour {
    public Train trainOnStation;
    [HideInInspector]
    public int pickUpItems;
   // public bool canPickUp = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {		
	}
    void TrainStopedAction()
    {
        UIController.Instance.ActivatePickUpButton(delegate ()
        {
            GamePlayController.Instance.AddScore(pickUpItems);
            UIController.Instance.DeactivatePickUpButton();
            trainOnStation.Stop -= TrainStopedAction;
            trainOnStation.Go -= TrainGoAction;
            // canPickUp = false;
        });
    }
    void TrainGoAction()
    {
        UIController.Instance.DeactivatePickUpButton();
        //trainOnStation.Go -= TrainGoAction;
    }
    void OnTriggerEnter(Collider col)
    {
        Train train = col.GetComponent<Train>();
        if (train != null)
        {
            trainOnStation = train;
            trainOnStation.Stop += TrainStopedAction;
            trainOnStation.Go += TrainGoAction;
        }
    }
    void OnTriggerExit(Collider col)
    {
        Train train = col.GetComponent<Train>();
        if (train != null)
            if(train == trainOnStation)
            {
                trainOnStation.Stop -= TrainStopedAction;
                trainOnStation.Go -= TrainGoAction;
                trainOnStation = null;
           //     canPickUp = true;
            }
    }
}
