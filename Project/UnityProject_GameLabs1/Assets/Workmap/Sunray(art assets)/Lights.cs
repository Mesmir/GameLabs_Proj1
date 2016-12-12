using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour {

	public Light lt;

	[Header("De Min/Max licht van de lamp")]
	public float normalLightIntensity;
	public float blinkLightIntensity;

	[Header("Snelheid van het blinken")]
	public float minSpeedBlink;
	public float maxSpeedBlink;

	public bool enabled = true;
	void Start()
	{
		lt = GetComponent<Light>();
		StartCoroutine (Blink());
	}

	private IEnumerator Blink(){

		bool _enabled = true;
		while (enabled) {
			float timeToBlink = Random.Range (minSpeedBlink,maxSpeedBlink);
			_enabled = !_enabled;
			if (_enabled)
				lt.intensity = normalLightIntensity;
			else
				lt.intensity = blinkLightIntensity;
			yield return new WaitForSeconds (timeToBlink);
		}
	}
		
}
