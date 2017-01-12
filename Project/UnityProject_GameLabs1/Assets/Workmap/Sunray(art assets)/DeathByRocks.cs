using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByRocks : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
            c.GetComponent<Stats_Player>().OnDeath();
    }
}
