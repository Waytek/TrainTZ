using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour {
    public StationCollider stationCollider;
    public int pickUpItem = 1;
	// Use this for initialization
	void Start () {
        stationCollider.pickUpItems = pickUpItem;
    }
}
