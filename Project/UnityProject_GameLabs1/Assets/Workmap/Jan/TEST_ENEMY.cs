using UnityEngine;
using System.Collections;

public class TEST_ENEMY : MonoBehaviour {

    public int hp = 100;

	void OnTriggerStay(Collider x)
    {
        if (x.transform.tag == "Player") {
            Combat combat = x.GetComponent<Combat>();
            if (combat.currentStatus == Combat.CharacterStatus.Comboing && x.GetComponent<AttackData>().damageFrames)
            {
                print("AU");
                hp -= combat.combos[combat.currentCombo].damage;
                x.GetComponent<AttackData>().damageFrames = false;
                if (hp < 80)
                    GetComponent<Renderer>().material.color = Color.yellow;
                if (hp < 50)
                    GetComponent<Renderer>().material.color = Color.red;
                if(hp < 1)
                    Destroy(gameObject);
            }
        }
    }
}
