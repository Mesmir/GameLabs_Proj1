﻿using UnityEngine;
using System;

public class Combo : MonoBehaviour {

    [Serializable]
    public class combos
    {
        public string name; //ook de naam van de state
        public bool unlocked = false;
        public int damage;
        public string[] comboString;
        public int staminaCost;
        public GameObject[] damageAreas;
        public ParticleEffect[] particles;
        public bool isRanged;
        public GameObject projectile;
        public Transform projectileVector;
        public AudioClip playerSND;
    }
    [Serializable]
    public class ParticleEffect
    {
        public Vector3 offset;
        public GameObject particle;
        public float duration;
    }
}
