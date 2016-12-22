using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AttackData), typeof(Animator))]
public class EnemyBase : MonoBehaviour, IEnemy {

    #region References
    public Enemy.Enemy_Class.Enemy stats;
    private Animator anim;

    #region State References

    public string walkStateName;
    public string deathStateName;

    public enum State {Walk, Attack }
    private State currentState = State.Walk;

    [HideInInspector]
    public int currentCombo;

    public bool looksRight;

    #endregion

    #endregion

    #region Base AI Behaviour

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Move();
    }

    #endregion

    #region Move

    private void Move()
    {
        if (currentState == State.Walk)
        {
            RaycastHit hit;
            Vector3 playerPos = GameHandler._Player.transform.position;
            playerPos.y += 1;
            Debug.DrawLine(transform.position, playerPos, Color.red);
            if (Physics.Raycast(transform.position, playerPos, out hit, stats.noticeRange))
            {
                print(hit.transform.tag);
                if (hit.collider.tag == "Player")
                {
                    #region Check If Right Rotation

                    if ((looksRight && hit.transform.position.x < transform.position.x) ||
                        (!looksRight && hit.transform.position.x > transform.position.x))
                    {
                        Quaternion newRot = Quaternion.LookRotation(-transform.forward, Vector3.up);
                        transform.rotation = newRot;
                        looksRight = !looksRight;
                    }

                    #endregion
                    
                    float dis = Vector3.Distance(transform.position, hit.transform.position);
                    if (dis < stats.attackRange)
                    {
                        #region Attack Player

                        List<int> attacks = new List<int>();
                        for (int attack = 0; attack < stats.attacks.Length; attack++)
                        {
                            Enemy.Enemy_Class.EnemyAttack curAttack = stats.attacks[attack];
                            if (dis < curAttack.maxRange && dis > curAttack.minRange)
                                attacks.Add(attack);
                        }

                        #region Choose Attack

                        if (attacks.Count == 0)
                            return;

                        int chosenAttack = Random.Range(0, attacks.Count);
                        Attack(attacks[chosenAttack]);

                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region Move Towards Player

                        transform.Translate(transform.forward * (stats.speed * Time.deltaTime), Space.World);

                        #endregion
                    }
                    
                }
            }
        }
    }

    #endregion

    protected string currentAttack;

    public virtual void Attack(int combo)
    {
        currentCombo = combo;
        currentAttack = stats.attacks[combo].name;
        anim.SetTrigger(currentAttack);
    }

    public virtual void EndAttack()
    {
        currentState = State.Walk;
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
                    ChangeHP(-c.combos[c.currentCombo].damage);
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
