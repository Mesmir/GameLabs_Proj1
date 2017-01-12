using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

    private GameObject player;
    private Rigidbody playerRb;
    private Animator animatorPlayer;
    public float dashSpeed = 20f;
    private bool goTimer;
    public float timer = 0.2f;
    private float tmr;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
        animatorPlayer = player.GetComponent<Movement>().animatorPlayer;
        tmr = timer;
    } 
    public void Update()
    {
        SkillOne();
    }
 
    public void SkillOne ()
    {
        #region Timer to stop the velocity.
        if (goTimer)
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            goTimer = false;    
            playerRb.velocity = new Vector3(0, 0, 0);
            playerRb.angularVelocity = new Vector3(0, 0, 0);
            timer = tmr;
        }
        #endregion

        #region "Skill itself".
        if (Input.GetButton("Fire1"))
        {
            goTimer = true;
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
        }
        if (playerRb.velocity == new Vector3(0, 0, 0))
            animatorPlayer.speed = 1;
        #endregion
    }
}
