using UnityEngine;
using System.Collections;

public class HintActivation : MonoBehaviour {

    public GameObject player;
    public UnityEngine.UI.Text textObject;
    public string hint;
    public float timer;

    HintActive hintSc;

    void Start () {

        hintSc = player.GetComponent<HintActive>();
    }

    void OnTriggerEnter ( Collider trigger ) { 

        if( trigger.gameObject.tag == "Player")
        {
            textObject.text = hint;
            StartCoroutine(Timer());
            hintSc.startHints = true;
        }
    }

    IEnumerator Timer() {

        yield return new WaitForSeconds(timer);

        Destroy(this.gameObject);
    }
}
