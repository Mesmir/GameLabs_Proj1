using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMovement : MonoBehaviour {

    public float minY, maxY;
    [Header("Als je hem false laat start hij met een pad naar beneden, anders naar boven.")]
    public bool startMovingUp;
    public float speed;
    private int direction = 1;

    private void Start() {
        if (startMovingUp)
            direction = 1;
    }

    private void Update()
    {
        CheckMinAndMax();
        Move();
    }

    private void CheckMinAndMax()
    {
        Vector3 pos = transform.position;
        if (pos.y >= maxY)
            direction = -1;
        if (pos.y <= minY)
            direction = 1;
    }

    private void Move()
    {
        transform.Translate(transform.up * direction * Time.deltaTime * speed);
    }
}
