﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ability : MonoBehaviour {

    private GameObject player;
    private Rigidbody playerRb;
    private Animator animatorPlayer;
    [Header("Ability One")]
    public int abilityCostOne;
    public float AbilityOneCooldown;
    public float dashSpeed = 20f;
    private bool goTimer;
    public float timer = 0.2f;
    private float tmr;
    private bool ableToUse;
    private GameObject cdMaskObject;
    private Image cdMask;

    private AttackData attackData;

    public void Awake()
    {
        cdMaskObject = GameObject.FindWithTag("cdMaskOne");
        cdMask = cdMaskObject.GetComponent<Image>();
        ableToUse = true;
        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
        animatorPlayer = player.GetComponent<Movement>().animatorPlayer;
        tmr = timer;
        attackData = GetComponent<AttackData>();
    } 
    public void Update()
    {
        SkillOne();
    }
 
    public void SkillOne ()
    {
        if (gameObject.GetComponent<Stats_Player>().stamina <= abilityCostOne)
            cdMask.fillAmount = 1;

        #region Timer to stop the velocity.

        if (goTimer)
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            goTimer = false;    
            playerRb.velocity = new Vector3(0, 0, 0);
            playerRb.angularVelocity = new Vector3(0, 0, 0);
            timer = tmr;
            attackData.SwitchInvisibilityFrames();
        }

        #endregion

        #region "Skill itself".

        if (Input.GetButton("SkillOne"))
        {
            float stam = player.GetComponent<Stats_Player>().stamina;
            if (stam >= abilityCostOne && (stam - abilityCostOne) > 0)
                if (ableToUse)
                {
                    goTimer = true;
                    //if(Mathf.Approximately(270, player.transform.eulerAngles.y))
                    if (player.transform.eulerAngles.y > 100)
                    {
                        playerRb.velocity = Vector3.left * dashSpeed;
                        animatorPlayer.Play("Jump", -1, 1.0f);
                        animatorPlayer.speed = 0;
                    }
                    //else if(Mathf.Approximately(90, player.transform.eulerAngles.y))
                    else if (player.transform.eulerAngles.y < 100)
                    {
                        playerRb.velocity = Vector3.right * dashSpeed;
                        animatorPlayer.Play("Jump", -1, 1.0f);
                        animatorPlayer.speed = 0;
                    }
                    player.GetComponent<Stats_Player>().stamina -= abilityCostOne;

                    #region "Cooldown".

                    attackData.SwitchInvisibilityFrames();
                    if(stam >= abilityCostOne)
                        StartCoroutine("SkillOneCooldown");

                    #endregion
                }
        }
        if (playerRb.velocity == new Vector3(0, 0, 0))
            animatorPlayer.speed = 1;

        #endregion
    }

    IEnumerator SkillOneCooldown ()
    {
        float f = AbilityOneCooldown;
        while(f > 0)
        {
            if (gameObject.GetComponent<Stats_Player>().stamina <= abilityCostOne)
            {
                yield break;
            }

            f -= Time.deltaTime;
            cdMask.fillAmount = (f / AbilityOneCooldown);
            ableToUse = false;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ableToUse = true;

    }
}
