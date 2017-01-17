using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickUp : MonoBehaviour {

    private GameObject script;
    public GameObject image;

    private void Start()
    {
        script = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            script.GetComponent<Ability>().enabled = true;
            image.SetActive(false);
            Destroy(gameObject);
        }
    }
}
