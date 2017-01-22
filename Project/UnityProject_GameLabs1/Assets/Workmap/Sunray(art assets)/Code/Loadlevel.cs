using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadlevel : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Application.LoadLevel("Street");
	}
}