//tags needed: "Ground", "Jumpable". input needed: "Horizontal", "Jump", "Crouch".
//!!!NOTE!!! To be able to crouch below something, it needs to be 0.1f higer than the collider!

using UnityEngine;
using System.Collections;													

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody playerRb;
	public float speed = 7f;
    public float jumpHeight = 3f;
	public bool faceRight = true;	
	private float moveUpDown;
	private int numberJumps;
	public int currentJumps = 2;
	private bool onWall;
	public float timeWallStick = 0.2f;
	private float tWS;
	private Vector3 wallRotation;
	private float height;
    public bool onGround;
	
	void Start () 
	{
		playerRb = GetComponent<Rigidbody>();
		numberJumps = currentJumps;
		tWS = timeWallStick;
		height = GetComponent<CapsuleCollider>().height;
    }
	
	void FixedUpdate () 
	{
        if (!GameObject.FindWithTag("Player").GetComponent<Player_Script>().inCombo)
        {
            Walking();
            Jumping();
            if (onGround)
            {
                Crouching();
            }
            if (onWall == true)
            {
                WallJumpTimer();
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
	
	public void Walking ()
	{
		float moveZ;
		moveZ = Input.GetAxis ("Horizontal");
		if(moveZ > 0.05)
		{
			if(onWall == false)	
			{
				if(faceRight == false)
				{
					transform.Rotate(0,180,0);
					faceRight = true;
				}
			}
			transform.Translate(transform.right * (speed * Time.deltaTime));
		}
		else if(moveZ < -0.05)
		{
			if(onWall == false)	
			{
				if(faceRight == true)
				{
					transform.Rotate(0,180,0);
					faceRight = false;
				}
			}
			transform.Translate(-transform.right * (speed * Time.deltaTime));
		}
	}
	
	public void Jumping ()
	{
		if(Input.GetButtonDown("Jump"))
		{
			if(currentJumps > 0)
			{
				if(playerRb.useGravity == false)
				{
					playerRb.useGravity = true;
				}
				currentJumps--;
				if(currentJumps < numberJumps)
				{
					playerRb.velocity = Vector3.zero; 
					playerRb.AddForce(transform.up * (jumpHeight * 100));
				}
			}
		}
	}

	public void WallJumpTimer ()
	{
		timeWallStick -= Time.deltaTime;
		if(timeWallStick <= 0)
		{
			onWall = false;
			playerRb.useGravity = true;
			timeWallStick = tWS;
		}
	}
	
	public void Crouching ()
	{
		Vector3 location = new Vector3(0,0,0);
		if(Input.GetButton("Crouch"))
		{
			GetComponent<CapsuleCollider>().height = height / 2;
			location.y = -(height /4 );
			GetComponent<CapsuleCollider>().center = location;
		}
		else
		{
			if(!Physics.Raycast(transform.position, transform.up, height) && !Input.GetButton("Crouch"))
			{
				GetComponent<CapsuleCollider>().height = height;
				location.y = 0;
				GetComponent<CapsuleCollider>().center = location;
			}
		}
	}

	public void OnCollisionEnter (Collision infoEn)
	{
		if(infoEn.collider.tag == "Ground")
		{
			currentJumps = numberJumps;
            onGround = true;
		}
        if (onGround == false)
        {
            if (infoEn.collider.tag == "Jumpable")
            {
                onWall = true;
                playerRb.velocity = Vector3.zero;
                playerRb.useGravity = false;
                if (currentJumps < numberJumps)
                {
                    currentJumps++;
                }
                if (faceRight == true)
                {
                    faceRight = false;
                    wallRotation.y = 180;
                    transform.eulerAngles = wallRotation;
                }
                else if (faceRight == false)
                {
                    faceRight = false;
                    wallRotation.y = 0;
                    transform.eulerAngles = wallRotation;
                }
            }
        }
	}
	
	public void OnCollisionExit (Collision infoEx)
	{
		if(infoEx.collider.tag == "Jumpable")
		{
			playerRb.useGravity = true;
			Quaternion temp = transform.rotation;
			temp.y += 180;
			transform.rotation = temp;
			onWall = false;
			timeWallStick = tWS;
		}
        if(infoEx.collider.tag == "Ground")
        {
            onGround = false;
        }
	}

	
}