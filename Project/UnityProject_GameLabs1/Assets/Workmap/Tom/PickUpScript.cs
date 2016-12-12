using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

    public string comboName;

    private void OnTriggerEnter(Collider info)
    {
        if(info.tag == "Player")
        {
            Combat c = info.gameObject.GetComponent<Combat>();
            foreach (Combo.combos combo in c.combos)
                if (combo.name == comboName)
                {
                    combo.unlocked = true;
                    Destroy(gameObject);
                }
            print("Combo does not excist.");
        }
    }
}
