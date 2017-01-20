using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood_PickUp : MonoBehaviour {

    public int staminaGain = 100;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Stats_Player>().stamina += staminaGain;
            Destroy(gameObject);
        }
    }
}
