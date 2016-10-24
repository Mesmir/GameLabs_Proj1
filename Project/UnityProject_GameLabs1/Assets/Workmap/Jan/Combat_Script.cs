using UnityEngine;
using System;
using System.Collections;

public class Combat_Script : MonoBehaviour {

    /*
    dit script wordt puur gebruikt om combo's in te verwijzen en hoeft dus niet in de scene gesleept te worden.
    er wordt gewoon een nieuwe instance van de combo class gemaakt en daarmee wordt gespeeld
    */
    [Serializable]
	public class Combo
    {
        public string name;
        public bool unlocked = false;
        public Animation anim;
        public int damage;
        public string[] comboString;

        public GameObject projectile; //voor ranged attacks, wordt dalijk verwezen in de player_script in een shoot functie, dan kan je in unity in de animatie de shoot gebruiken en dan kijkt het script welke aanval bezig is
        public Transform projectileVector; //hieruit schiet ie

        public Combo(Animation _anim, int _damage, string[] _comboString, string _name, bool _unlocked) //deze is dus voor attacks zonder projectile
        {
            name = _name;
            anim = _anim;
            damage = _damage;
            comboString = _comboString;
            unlocked = _unlocked;
        }
        public Combo(GameObject _projectile, Animation _anim, int _damage, string[] _comboString, string _name, bool _unlocked)
        {
            name = _name;
            projectile = _projectile;
            anim = _anim;
            damage = _damage;
            comboString = _comboString;
            unlocked = _unlocked;
        }
        public Combo(Transform _projectileVector)
        {
            projectileVector = _projectileVector;
        }
    }
}
