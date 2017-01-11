﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

    private GameObject player;
    private Rigidbody playerRb;
    private Animator animatorPlayer;
    public float dashSpeed = 5f;
    public float testz;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
        animatorPlayer = player.GetComponent<Movement>().animatorPlayer;
    } 
    public void Update()
    {
        SkillOne();
    }
 
	public void SkillOne ()
    {
        if(Input.GetButton("Fire1"))
        {
            float t = 0;
            t += 1 * Time.deltaTime;
            if (player.transform.eulerAngles.y == 270)
            {
                playerRb.velocity = Vector3.left * dashSpeed;
                animatorPlayer.Play("Jump", -1, 1.0f);
                animatorPlayer.speed = 0;
            }
            else if (player.transform.eulerAngles.y == 90)
            {
                playerRb.velocity = Vector3.right * dashSpeed;
                animatorPlayer.Play("Jump", -1, 1.0f);
                animatorPlayer.speed = 0;
            }
            if(t >= testz)
            {
                playerRb.velocity = new Vector3(0, 0, 0);
                t = 0;
            }
        }
        if(playerRb.velocity == new Vector3(0,0,0))
        {
            animatorPlayer.speed = 1;
        }
    }
}
