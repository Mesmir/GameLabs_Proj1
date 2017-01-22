using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate : MonoBehaviour {

	public GameObject target;
	private float ogPosX, ogPosY, ogPosZ;
	public float amountUp;

	// Use this for initialization
	void Start () {
		ogPosX = target.transform.position.x;
		ogPosY = target.transform.position.y;
		ogPosZ = target.transform.position.z;
	}

	void OnTriggerEnter(Collider other) {
		target.transform.position = new Vector3 (ogPosX, ogPosY + amountUp, ogPosZ);
	}
}
