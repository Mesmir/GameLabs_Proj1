using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate : MonoBehaviour {

	public GameObject target, handle;
	private float ogPosX, ogPosY, ogPosZ;
	public float amountUp;
	private bool activated;
	private bool keyPressed;
	public float up;


	// Use this for initialization
	void Start () {
		ogPosX = target.transform.position.x;
		ogPosZ = target.transform.position.z;
	}

	void Update () {

		ogPosY = target.transform.position.y;

		if(target.transform.position.y > amountUp && keyPressed == true){
			Destroy(gameObject);
		}

		if(Input.GetKeyDown ("e") && activated == true){
			RotateHandle ();
			keyPressed = true;
		}

		if(target.transform.position.y < amountUp && keyPressed == true){
			MoveGateUp();
		}
	}

	void OnTriggerEnter(Collider other) {

		activated = true;

	}

	void RotateHandle(){
		handle.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 50, transform.eulerAngles.z);
	}


	void MoveGateUp ()
	{
		target.transform.position = new Vector3 (ogPosX, ogPosY+up, ogPosZ);
	}
}
