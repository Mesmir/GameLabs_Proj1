using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// will nog een damping follow naar de player's Y as erbij doen.
public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Camera camera;

    public float LookSpeed;
    public float followSpeedX;
    public float followSpeedY;
    public bool posXCheck;
   
    void Start()
    {
        player = GameHandler._Player.transform;
    }

    void FixedUpdate()
    {
        //Vector3 viewPos = camera.WorldToViewportPoint(player.position);
        Vector3 direction = player.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(direction);
        rot.z = 0.0f;
        rot.y = 0.0f;
        //transform.rotation = Quaternion.Lerp(transform.rotation, rot, LookSpeed);

        if (player.position.y > transform.position.y + 2 || player.position.y < transform.position.y)
        {
            Vector3 tempPosOfY = transform.position;
            tempPosOfY.y = player.position.y + 1.5f;
            transform.position = Vector3.MoveTowards(transform.position, tempPosOfY, followSpeedY);
        }

        //if (mayMoveRight == true /*&& viewPos.x > 0.25f || viewPos.x < 0.75f*/)
        //{
            Vector3 tempPosOfX = transform.position;
            tempPosOfX.x = player.position.x;
            transform.position = Vector3.MoveTowards(transform.position, tempPosOfX, followSpeedX);
            //Debug.Log("you may move");
        //}
        //if (rightBorderCheck.x < 1 && viewPos.x > 0.45f || leftBorderCheck.x > 0 && viewPos.x < 0.55f)// als de rightborder in view komt stopt ie, check of de speler aan de linkerkant van het scherm is om terug te gaan.
        //{   //kan ook wel met distance
        //    mayMoveRight = false;
        //    Debug.Log("Aborderisinrange");
        //}
        //else
        //{
        //    mayMoveRight = true;
        //}
    }
}


