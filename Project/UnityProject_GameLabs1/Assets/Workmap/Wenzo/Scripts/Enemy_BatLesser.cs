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
    public Transform player;
    public float rayCheckRange;
    RaycastHit hit;

    //protected override GameObject FindPlayer()
    //{
    //    return base.FindPlayer();
    //}

    //protected override bool ReceiveDamage(int damage, int remainingHP)
    //{
    //    return base.ReceiveDamage(damage, remainingHP);
    //}

    public void OnTriggerEnter(Collider onCol)
    {
        if (onCol.tag == "Player")
        {
            isChasing = true;
            StartCoroutine(Chase());
        }
        print("entering zone");
    }

    public void OnTriggerExit()
    {
        StopCoroutine(Chase());
        isChasing = false;
        print("Trying to stop it");
    }

    public IEnumerator Chase()
    {
        while (isChasing)
        {
            Debug.DrawRay(transform.position, player.position, Color.red, 3f);
            if (Physics.Raycast(transform.position, player.position, out hit, rayCheckRange))
            {
                print("Shootin rays");
                Debug.Log(hit.transform.name);
            }
            print("Still Chasing");
            yield return false;
        }
        print("still going on?");
    }
}
