using UnityEngine;
using System.Collections;

public class Multiplier_Damage : MonoBehaviour {

    private int maxAddedDamageInPercent = 100;
    [Range(0,100)]
    public int addedDamagePerKillInPercent;
    private int currentMultiplier;
    public int CurrentMultiplier //added percent
    {
        get
        {
            return currentMultiplier * addedDamagePerKillInPercent;
        }
    }
    public float timerMax;
    private float timer;

    void Start()
    {
        timer = timerMax;
    }

    public int KillMade()
    {
        if ((currentMultiplier + 1) * addedDamagePerKillInPercent < maxAddedDamageInPercent)
            currentMultiplier++;
        timer = timerMax;
        StartCoroutine(Countdown());
        return CurrentMultiplier;
    }

    private IEnumerator Countdown()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        currentMultiplier = 0;
    }
}
