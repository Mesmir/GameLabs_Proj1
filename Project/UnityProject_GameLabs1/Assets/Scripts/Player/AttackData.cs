using UnityEngine;
using System.Collections;

public class AttackData : MonoBehaviour {

    public bool invincibleFrames;
    public bool damageFrames;
    public Transform[] damageAreas;

    public void SwitchInvisibilityFrames()
    {
        invincibleFrames = !invincibleFrames;
    }

    public void DealsDamage()
    {
        damageFrames = !damageFrames;
    }

    public void ShootProjectile()
    {
        //schiet projectile uit huidige combo
    }

    public void ActivateParticles()
    {
        //activeer particles
    }
}
