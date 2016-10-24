using UnityEngine;
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

            public Enemy(Animation _idle, Animation _walking)
            {
                idle = _idle;
                walking = _walking;
            }

            public Enemy(int _hp, float _speed, float _noticeRange, EnemyAttack[] _attacks)
            {
                hp = _hp;
                speed = _speed;
                noticeRange = _noticeRange;
                attacks = _attacks;
            }
            public Enemy(int _hp, float _speed, float _noticeRange, EnemyAttack[] _attacks, float _drainRange, int _drainSpeed)
            {
                hp = _hp;
                speed = _speed;
                noticeRange = _noticeRange;
                attacks = _attacks;
                drainRange = _drainRange;
                drainSpeed = _drainSpeed;
                drainsStamina = true;
            }
            public Enemy(float _drainRange, int _drainSpeed) //add draining
            {
                drainRange = _drainRange;
                drainSpeed = _drainSpeed;
                drainsStamina = true;
            }
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

            public EnemyAttack(string _name, bool _unique)
            {
                name = _name;
                unique = _unique;
            }

            public EnemyAttack(int _damage, int _damageStamina, float _minRange, float _maxRange, Animation _attack)
            {
                damage = _damage;
                damageStamina = _damageStamina;
                minRange = _minRange;
                maxRange = _maxRange;
                attack = _attack;
            }

            public EnemyAttack(int _damage, int _damageStamina, float _minRange, float _maxRange, Transform _projectile, Animation _attack, Transform _projectileVector)
            {
                damage = _damage;
                damageStamina = _damageStamina;
                minRange = _minRange;
                maxRange = _maxRange;
                projectile = _projectile;
                attack = _attack;
                projectileVector = _projectileVector;
            }
        }
    }
}