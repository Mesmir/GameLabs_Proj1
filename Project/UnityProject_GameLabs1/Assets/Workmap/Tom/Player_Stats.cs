﻿using UnityEngine;
using System;
using System.Collections;

public class Player_Stats : MonoBehaviour
{

    public StaminaLevel[] staminaLevels;

    #region Stats

    [HideInInspector]
    public int stamina;
    private string lastStaminaLevel;
    private Combat combat;

    #region InspectorSettings

    [Range(0, 1000)]
    public int hp = 1000;
    public int maxStamina;
    public int lossStaminaPerSec;
    public int[] hpRegen;

    #endregion

    #endregion

    private int currentStaminaLevel;

    private void Awake()
    {
        stamina = maxStamina;
        combat = GetComponent<Combat>();
        InvokeRepeating("TickStamina", 0, 1f);
        hpRegen = new int[staminaLevels.Length];
    }

    #region Health

    public void RegenHealth()
    {
        hp += hpRegen[staminaLevels.Length];                                        ///////////////////////////////////
    }

    public void ChangeHealth(int damage)
    {
        hp += damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (hp < 1)
            OnDeath();
    }

    public void OnDeath()
    {
        print("ded");
        //SAVE MET XMLMANAGER

        //reload level
    }

    private void OnTriggerStay(Collider x) //trigger lijkt me handiger, maar als je een manier vindt om dit in een collission te veranderen, be my guest
    {
        if (x.transform.tag == "Weapon")
        {
            if (x.transform.root.tag != "Enemy")
                return;

            Transform enemy = x.transform.root;
            IEnemy i = enemy.GetComponent(typeof(IEnemy)) as IEnemy;
            AttackData a = enemy.GetComponent<AttackData>();
            if (i.GetState() == EnemyBase.State.Attack)
                if (a.damageFrames)
                {
                    a.damageFrames = false;
                    ChangeHealth(-i.GetAttackDamage());
                    i.DoesDamage(false);
                }
        }
    }

    #endregion

    #region Stamina

    private void TickStamina()
    {
        ChangeStamina(-lossStaminaPerSec);
    }

    public void ChangeStamina(int excaustion)
    {
        stamina += excaustion;
        CheckStamina();
    }

    private void CheckStamina()
    {
        int pastS = currentStaminaLevel - 1;
        if (pastS < 0)
            pastS++;
        int nextS = currentStaminaLevel + 1;
        if (nextS > staminaLevels.Length)
            pastS--;
        if (stamina < pastS || stamina > nextS)
            for (int staminaLvl = 0; staminaLvl < staminaLevels.Length; staminaLvl++)
                if (stamina < staminaLevels[staminaLvl].staminaLevel)
                {
                    foreach (int combo in staminaLevels[staminaLvl].unlockedCombos)
                        combat.combos[combo].unlocked = false;
                    currentStaminaLevel = staminaLvl;
                    return;
                }
                else
                    foreach (int combo in staminaLevels[staminaLvl].unlockedCombos)
                        combat.combos[combo].unlocked = true;
    }

    #endregion

    #region Objects

    [Serializable]
    public class StaminaLevel
    {
        public string name;
        public int staminaLevel;
        public int[] unlockedCombos; //welk nummer in de combo list
    }

    #endregion
}
