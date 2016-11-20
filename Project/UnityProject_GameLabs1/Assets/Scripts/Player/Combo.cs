using UnityEngine;
using System;

public class Combo : MonoBehaviour {

    [Serializable]
    public class combos
    {
        public string name;
        public bool unlocked = false;
        public AnimationClip anim;
        public int damage;
        public string[] comboString;
        public int staminaCost;
        public GameObject[] damageAreas;
        public ParticleEffect[] particles;
        public bool isRanged;
        public GameObject[] projectile;
        public Transform projectileVector;
    }
    [Serializable]
    public class ParticleEffect
    {
        public Vector3 offset;
        public GameObject particle;
        public float duration;
    }
}
