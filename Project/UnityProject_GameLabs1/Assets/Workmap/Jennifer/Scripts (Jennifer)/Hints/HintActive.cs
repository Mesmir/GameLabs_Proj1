using UnityEngine;
using System.Collections;

public class HintActive : MonoBehaviour {


    public float timerFloat;
    public UnityEngine.UI.Text hintText;
    public bool startHints;

    void Update()
    {
        if (startHints)
            StartCoroutine(Timer());
    }

    IEnumerator Timer() {

        hintText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timerFloat);

        hintText.gameObject.SetActive(false);
        startHints = false;

    }
}
