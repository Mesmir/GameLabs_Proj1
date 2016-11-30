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

    public void DealsDamage(int doesDamage)
    {
        if (doesDamage == 0)
            damageFrames = false;
        else
            damageFrames = true;
    }

    public void ShootProjectile()
    {
        GameObject projectiles = combat.combos[combat.currentCombo].projectile;
        //schiet hem uit de voorkant van de speler, uit currentcombo.projectileVector
    }

    public void ActivateParticles()
    {
        //activeer particles, voor later niet voor deze build
    }
}
