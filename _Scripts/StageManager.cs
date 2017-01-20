using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {

    public GameObject manager;

	// Use this for initialization
	void Start () {
        Instantiate(manager).GetPhotonView().viewID = 1;
        
	}
}
