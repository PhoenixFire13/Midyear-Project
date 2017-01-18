using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {

    void OnTriggerEnter (Collider other) {
        Destroy(other.gameObject);
    }	

}
