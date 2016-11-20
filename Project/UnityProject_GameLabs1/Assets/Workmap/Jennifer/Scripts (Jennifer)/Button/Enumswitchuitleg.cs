using UnityEngine;
using System.Collections;

public class Enumswitchuitleg : MonoBehaviour {

    public enum Compass {North = 0, South = 180, East = 90, West = 270 }

    public Compass richting;

	// Use this for initialization
	void Start () {
        richting = Compass.North;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
