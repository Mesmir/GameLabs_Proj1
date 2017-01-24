using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadlevel2 : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Application.LoadLevel("End");
	}
}