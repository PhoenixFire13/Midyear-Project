using UnityEngine;
using System.Collections;

public class StrongAttack : MonoBehaviour {

    public int damage;

    // --------------- Figure out how to identify opponent ---------------
    private void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.name);
    }

    // --------------- Knockback distance depending on damage already dealt to opponent ---------------
    /*
    private int CalculateKnockback()
    {

    }
    */

}
