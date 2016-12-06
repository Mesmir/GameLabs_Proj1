using UnityEngine;
using System.Collections;

public class AttackData : MonoBehaviour {

    [HideInInspector]
    public bool invincibleFrames = false;
    [HideInInspector]
    public bool damageFrames = false;

    public bool isPlayer; //anders enemy
    private Combat combat;

    private void Awake()
    {
        combat = GetComponent<Combat>();
    }

    public void SwitchInvisibilityFrames()
    {
        invincibleFrames = !invincibleFrames;
    }

    public void DealsDamage(int doesDamage) //ik mag geen bool gebruiken in events, dus doe ik het zo. beetje bs maar dit is de makkelijkste manier
    {
        if (doesDamage == 0)
            damageFrames = false;
        else
            damageFrames = true;
    }

    public void ActivateParticles()
    {
        //activeer particles, als we dat ooit nog gaan gebruiken
    }
}
