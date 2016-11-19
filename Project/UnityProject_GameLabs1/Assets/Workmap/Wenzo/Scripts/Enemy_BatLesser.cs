using UnityEngine;
using System.Collections;

// collider. OnTriggerEnter
// raycast naar speler.
// if hit, Vector3.Movetowards
// if distance < 1 GoBoom()
// else if dist > 10 && OutOfTrigger == true;
// MoveTowards, base Position.

//IEnumerator
//OnTriggerEnter, StartCoroutine, While true follow player

public class Enemy_BatLesser : Enemy_Base {

    public bool isChasing;
    //protected override GameObject FindPlayer()
    //{
    //    return base.FindPlayer();
    //}

    //protected override bool ReceiveDamage(int damage, int remainingHP)
    //{
    //    return base.ReceiveDamage(damage, remainingHP);
    //}

    public void OnTriggerEnter()
    {
        StartCoroutine (Chase());
    }

    public void OnTriggerExit()
    {
        StopCoroutine(Chase());
    }

    public IEnumerator Chase()
    {

        while (isChasing)
        {
            print("Still Chasing");
            yield return false;
        }
    }
}
