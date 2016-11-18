﻿using UnityEngine;
using System;
using System.Collections;

namespace Enemy
{
    public class Enemy_Class : MonoBehaviour
    {
        [Serializable]
        public class Enemy
        {
            public int hp;
            private bool drainsStamina = false; //kan met attacks op true worden gezet, sommige hebben het ook de hele tijd aan
            public float drainRange;
            public int drainSpeed;
            public float speed;
            public float noticeRange;
            public EnemyAttack[] attacks;  ///sunray

            public Animation idle;
            public Animation walking;
        }

        [Serializable]
        public class EnemyAttack
        {
            public string name;
            public bool unique;
            public int damage;
            public int damageStamina;
            public float minRange; //van hoever hij de attack kan doen, minimaal
            public float maxRange;
            public Transform projectile;
            public Transform projectileVector;
            public Animation attack;
        }
    }
}