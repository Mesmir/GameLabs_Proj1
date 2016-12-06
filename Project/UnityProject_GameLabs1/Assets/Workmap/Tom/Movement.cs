﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    /* Input tags list:
     * "Crouch"
     * "Ground"
     * "Jumpable"
    */

    public bool onGround;
    private RaycastHit hit;

    #region Variables that have to be adjusted BEFORE playing

    [Space(10), Header("Adjust before game starts!")]

    public Animator animatorPlayer;

    private Rigidbody rigidbodyPlayer;

    #endregion

    #region Variables that can be adjusted WHILE playing

    [Space(10), Header("Adjustments can be made while playing!")]

    public float movementSpeed;
    public float jumpHeight;

    // Number of jumps you have/can have
    public int maxJumps;
    private int jumps;

    // crouching
    public float colliderHeight = 4.7f;
    public float crouchColliderHeight;
    private bool crouching;

    // Timer for sticking on a wall (when timer runs out the player falls off the wall it was sticking to)
    public float wallStickTime;
    private float wST;
    private bool onWall;

    #endregion

    void Awake ()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        wST = wallStickTime;
        jumps = maxJumps;
        if(movementSpeed == 0)
            movementSpeed = 3;
        if (maxJumps == 0)
            maxJumps = 1;
    }

    void FixedUpdate ()
    {
        if (!GameObject.FindWithTag("Player").GetComponent<Player_Script>().inCombo)
        {
            Jumping();
            Moving();
            if (onGround)
                Crouching();
            else animatorPlayer.ResetTrigger("Idle");
            if (onWall)
                TimerWallJump();
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void Moving ()
    {
        float inputAxis;
        inputAxis = Input.GetAxis("Horizontal");
        Vector3 rotation;
        rotation = new Vector3(0, 0, 0);
        if (inputAxis >= 0.1)
        {
            if (!onGround)
                animatorPlayer.SetBool("Moving", false);
            else
            {
                animatorPlayer.SetBool("Moving", true);
                animatorPlayer.ResetTrigger("Idle");
            }
            transform.Translate(-transform.right * (movementSpeed * Time.deltaTime));
            rotation.y = 90;
            transform.eulerAngles = rotation;
        }
        else if (inputAxis <= -0.1)
        {
            if (!onGround)
                animatorPlayer.SetBool("Moving", false);
            else
            {
                animatorPlayer.SetBool("Moving", true);
                animatorPlayer.ResetTrigger("Idle");
            }
            transform.Translate(transform.right * (movementSpeed * Time.deltaTime));
            rotation.y = 270;
            transform.eulerAngles = rotation;
        }
        else if (inputAxis > -0.1 && inputAxis < 0.1)
        {
            animatorPlayer.SetBool("Moving", false);
            if(onGround)
                animatorPlayer.SetTrigger("Idle");
        }
    }

    public void Crouching ()
    {
        Vector3 positionCollider;
        positionCollider = new Vector3(0, 0, 0);
        if (Input.GetButton("Crouch"))
            crouching = true;
        else
            crouching = false;
        if (crouching == true)
        {
            if (crouchColliderHeight == 0)
                crouchColliderHeight = colliderHeight / 2;
            positionCollider = new Vector3(0f, (crouchColliderHeight / 2), 0f);
            GetComponent<CapsuleCollider>().height = crouchColliderHeight;
            GetComponent<CapsuleCollider>().center = positionCollider;
        }
        else
        {
            positionCollider = new Vector3(0f, (colliderHeight / 2), 0f);
            GetComponent<CapsuleCollider>().height = colliderHeight;
            GetComponent<CapsuleCollider>().center = positionCollider;
        }
    }

    public void Jumping ()
    {
        if (Input.GetButtonDown("Jump"))
            if(!crouching)
            {
                if (jumps > 0)
                {
                    jumps--;
                    if (onWall)
                    {
                        rigidbodyPlayer.useGravity = true;
                        onWall = false;
                        wST = wallStickTime;
                    }
                    else animatorPlayer.SetTrigger("Jumping");
                    rigidbodyPlayer.velocity = Vector3.zero;
                    rigidbodyPlayer.AddForce(transform.up * (jumpHeight * 100));
                }
            }
    }

    public void TimerWallJump ()
    {
        wST -= Time.deltaTime;
        if(wST <= 0)
        {
            onWall = false;
            rigidbodyPlayer.useGravity = true;
        }
    }

    void OnCollisionEnter (Collision enter)
    {
        if(!onGround)
            if (enter.collider.tag == "Jumpable")
            {
                    onWall = true;
                    rigidbodyPlayer.velocity = Vector3.zero;
                    rigidbodyPlayer.useGravity = false;
                    animatorPlayer.Play("Wall Jump", -1, 0.0f);
                    animatorPlayer.speed = 0;
                    if (jumps <= maxJumps)
                        jumps++;
            }
        if (enter.collider.tag == "Ground")
        {
            jumps = maxJumps;
            onGround = true;
            animatorPlayer.ResetTrigger("Jumping");
        }
    }

    void OnCollisionStay(Collision stay)
    {
        if (stay.collider.tag == "Jumpable")
            if (wST <= 0)
                if (!onWall)
                {
                    animatorPlayer.speed = 1;
                    animatorPlayer.Play("Jump", -1, 1.0f);
                    wST = wallStickTime;
                }
    }

    void OnCollisionExit (Collision exit)
    {
        if (exit.collider.tag == "Ground")
            onGround = false;
        if(exit.collider.tag == "Jumpable")
        {
            onWall = false;
            rigidbodyPlayer.useGravity = true;
            wST = wallStickTime;
            animatorPlayer.speed = 1;
            animatorPlayer.Play("Jump", -1, 0.1f);
            if(wST <= 0)
                wST = wallStickTime;
        }
    }
}