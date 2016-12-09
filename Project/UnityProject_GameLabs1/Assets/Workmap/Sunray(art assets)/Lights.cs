using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour {

	[Range(0,1000)]
	public float intensity;
	public Light lt;
	void Start()
	{
		lt = GetComponent<Light>();
	}
	void Update()
	{
		lt.intensity = intensity;
	}
}
