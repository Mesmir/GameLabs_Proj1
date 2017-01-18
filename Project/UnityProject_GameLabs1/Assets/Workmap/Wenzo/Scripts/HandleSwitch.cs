using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSwitch : MonoBehaviour {

    public Animator targetAnimator;
    public string targetAnimatorParameterName;
    public AudioSource targetSND;
    public Animator myAnimator;
    public string myAnimatorParameterName;
    public AudioSource mySND;

    void Update () {
		

	}

    void OnTriggerStay(Collider onCol)
    {
        if (onCol.transform.tag == "Player")
        {
            if (Input.GetButtonDown("Use"))
            {
                myAnimator.SetTrigger(myAnimatorParameterName);
                targetAnimator.SetTrigger(targetAnimatorParameterName);
                targetSND.Play();
                Debug.Log("A Gate is opening");
            }
        }
    }
}
