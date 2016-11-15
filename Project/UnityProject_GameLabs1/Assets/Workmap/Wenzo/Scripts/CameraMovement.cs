using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// will nog een damping follow naar de player's Y as erbij doen.
public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform rightBorder;
    public Transform leftBorder;
    public bool mayMoveRight;
    public bool mayMoveLeft;
    public Camera camera;

    public float LookSpeed;
    public float followSpeedX;
    public float followSpeedY;
    public bool posXCheck;
   
    void FixedUpdate()
    {
        Vector3 rightBorderCheck = camera.WorldToViewportPoint(rightBorder.position);
        Vector3 leftBorderCheck = camera.WorldToViewportPoint(leftBorder.position);
        Vector3 viewPos = camera.WorldToViewportPoint(player.position);
        Vector3 direction = player.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(direction);
        rot.z = 0.0f;
        rot.y = 0.0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, LookSpeed);

        if (player.position.y > transform.position.y + 2 || player.position.y < transform.position.y)
        {
            Vector3 tempPosOfY = transform.position;
            tempPosOfY.y = player.position.y + 2;
            transform.position = Vector3.MoveTowards(transform.position, tempPosOfY, followSpeedY);
        }

        if (mayMoveRight == true /*&& viewPos.x > 0.25f || viewPos.x < 0.75f*/)
        {
            Vector3 tempPosOfX = transform.position;
            tempPosOfX.x = player.position.x;
            transform.position = Vector3.MoveTowards(transform.position, tempPosOfX, followSpeedX);
            Debug.Log("you may move");
        }
        if (rightBorderCheck.x < 1 && viewPos.x > 0.45f || leftBorderCheck.x > 0 && viewPos.x < 0.55f)// als de rightborder in view komt stopt ie, check of de speler aan de linkerkant van het scherm is om terug te gaan.
        {
            mayMoveRight = false;
            Debug.Log("Aborderisinrange");
        }
        else
        {
            mayMoveRight = true;
        }
        //else if (rightBorderCheck.x > 0)
        //{
        //    Debug.Log("Not in range of Right Border");
        //    mayMoveRight = true;
        //}

        //if (mayMoveRight == true && player.position.x > transform.position.x)
        //{
            
        //}



        //if (leftBorderCheck.x < 1)
        //{
        //    mayMoveLeft = false;
        //    Debug.Log("LeftBorderCheck");
        //}
        //else if (leftBorderCheck.x > 0)
        //{
        //    Debug.Log("Not in range of Left Border");
        //    mayMoveLeft = true;
        //}
        //if (mayMoveLeft == true && player.position.x < transform.position.x)
        //{
        //    Vector3 tempPosOfX = transform.position;
        //    tempPosOfX.x = player.position.x;
        //    transform.position = Vector3.MoveTowards(transform.position, tempPosOfX, moveSpeed);
        //}
    }
}



//if (viewPos.x > 0.6f)
//{
//    posXCheck = true;
//    if (posXCheck == true)
//    {
//        Vector3 tempPos = transform.position;
//        tempPos.x = player.position.x;
//        transform.position = Vector3.MoveTowards(transform.position, tempPos, moveSpeed);
//        print("target is on the right side!");
//    }
//    else
//    {
//        posXCheck = false;
//    }
//}

//else if (viewPos.x < 0.4f)
//{
//    Vector3 tempPos = transform.position;
//    tempPos.x = player.position.x;
//    transform.position = Vector3.MoveTowards(transform.position, tempPos, moveSpeed);
//    print("target is on the right side!");
//}