using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleSwitch : MonoBehaviour {

    public Animator targetAnimator;
    public string targetAnimatorParameterName;
    public AudioSource targetSND;
    public Animator myAnimator;
    public string myAnimatorParameterName;
    //public AudioSource mySND;
    [Space()]
    public Text uIText;
    public string uITextMessage;

    void OnTriggerEnter(Collider onCol)
    {
        if (onCol.transform.tag == "Player")
        {
            if (uIText.enabled != true)
            {
                uIText.enabled = true;
            }
            uIText.text = uITextMessage;
        }
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

    void OnTriggerExit (Collider onCol)
    {
        if(onCol.transform.tag == "Player")
        {
            uIText.text = uITextMessage = null;
            print("settings message to null");
        }
    }
}
