using UnityEngine;
using System.Collections;

public class AfterButton : MonoBehaviour {

    public GameObject connectedButton;
    ActButton actButton; 

    void Start () {

        actButton = connectedButton.GetComponent<ActButton>();
    }

    void OnTriggerEnter (Collider collision) {

        if( collision.gameObject.tag == "Player")
        {
            actButton.CloseDoor();
        }        
    }
}
