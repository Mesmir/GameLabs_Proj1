using UnityEngine;
using System.Collections;

public class ActButton : MonoBehaviour {

    public enum ButtonSetting { Timer, Trigger, Default }
    public ButtonSetting currentButton;
    public float timerFloat;
    public Animator buttonAnimation;
    public Animator usedAnimation;
    

    void OnTriggerStay(Collider collision) {

        if (collision.gameObject.tag == "Player")
        {
            switch (currentButton)
            {
                case ButtonSetting.Timer:
                    if (Input.GetButtonDown("Use"))
                    {         
                        StartCoroutine(Timer());
                    }
                    break;

                case ButtonSetting.Trigger:
                    if (Input.GetButtonDown("Use"))
                    {                        
                        buttonAnimation.SetTrigger("Active");
                        usedAnimation.SetTrigger("Active");
                    }
                    break;                
            }            
        }
    }

    IEnumerator Timer() {
        currentButton = ButtonSetting.Default;

        buttonAnimation.SetTrigger("Active");
        usedAnimation.SetTrigger("Active");

        yield return new WaitForSeconds(timerFloat);

        usedAnimation.SetTrigger("Deactive");

        yield return new WaitForSeconds(1);
        currentButton = ButtonSetting.Timer;
    }

    public void CloseDoor () {

        usedAnimation.SetTrigger("Deactive");
    }
}
