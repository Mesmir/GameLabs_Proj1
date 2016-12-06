using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AttackData), typeof(Animator))]
public class EnemyBase : MonoBehaviour, IEnemy {

    #region References
    public Enemy.Enemy_Class.Enemy stats;
    private Animator anim;

    #region State References

    public string walkStateName;
    public string deathStateName;

    public enum State {Walk, Attack }
    public State currentState = State.Walk;

    [HideInInspector]
    public int currentCombo;

    #endregion

    #endregion

    public virtual void Walk(bool right)
    {
        int modifier = 1;
        Quaternion rotation = transform.rotation;
        if (right)
        {
            rotation.y = 90;
            modifier = -1;
        }
        else
            rotation.y = -90;
        transform.rotation = rotation;
        transform.Translate(Vector3.left * modifier * Time.deltaTime);
        anim.SetBool(walkStateName, true);
    }

    protected string currentAttack;

    public virtual void Attack(int combo)
    {
        currentCombo = combo;
        currentAttack = stats.attacks[combo].name;
        anim.SetTrigger(currentAttack);
    }

    public virtual void ChangeHP(int addedHP)
    {
        stats.hp += addedHP;

        if (stats.hp < 1)
            Death();
    }

    public virtual void Death()
    {
        anim.SetBool(deathStateName, true); //hier zou je ook een trigger van kunnen maken, wat jij wilt renzo ;)

        //renzo have fun
    }

    private void OnTriggerEnter(Collider x) //trigger lijkt me handiger, maar als je een manier vindt om dit in een collission te veranderen, be my guest
    {
        if(x.transform.tag == "Player")
        {
            Transform player = x.transform;
            Combat c = player.GetComponent<Combat>();
            AttackData a = player.GetComponent<AttackData>();

            if (c.currentStatus == Combat.CharacterStatus.Comboing)
                if (a.damageFrames)
                {
                    a.damageFrames = false;
                    stats.hp -= c.combos[c.currentCombo].damage;
                }
        }   
    }

    public State GetState()
    {
        return currentState;
    }

    public int GetAttackDamage()
    {
        return stats.attacks[currentCombo].damage;
    }
}
