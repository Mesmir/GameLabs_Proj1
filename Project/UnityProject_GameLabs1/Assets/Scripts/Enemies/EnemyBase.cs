using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AttackData), typeof(Animator))]
public class EnemyBase : MonoBehaviour, IEnemy {

    public float f;
    #region References
    public Enemy.Enemy_Class.Enemy stats;
    private Animator anim;

    #region State References

    public string walkStateName;
    public string deathStateName;
    public string idleStateName;

    public enum State {Walk, Attack}
    private State currentState = State.Walk;

    [HideInInspector]
    public int currentCombo;

    public bool looksLeft;

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
            //vector3 ipv vector3 pos, en dan y hoger zetten
            Vector3 vec = transform.position;
            vec.y += 1;
            Debug.DrawLine(vec, playerPos, Color.red);
            if (Physics.Raycast(transform.position, playerPos, out hit, stats.noticeRange))
            {
                if (hit.collider.tag == "Player")
                {
                    #region Check If Right Rotation

                    if (looksLeft && hit.transform.position.x < transform.position.x - f)
                        SwitchRotation();
                    else if (!looksLeft && hit.transform.position.x > transform.position.x + f)
                        SwitchRotation();

                    #endregion

                    float dis = Vector3.Distance(vec, hit.transform.position);
                    if (dis < stats.attackRange)
                    {
                        print("Attack! range: " + stats.attackRange);

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
                        print("Moving");
                        transform.Translate(transform.forward * (stats.speed * Time.deltaTime), Space.World);
                        anim.SetBool(walkStateName, true);

                        #endregion
                    }
                }
            }
            /*                                                                                                          //Fix this shit, Jan :)
            else
            {
                anim.SetBool(idleStateName, true);
                anim.SetBool(walkStateName, false);
                print("Out of reach!");
            }
            */
        }
    }

    private void SwitchRotation()
    {
        Quaternion newRot = Quaternion.LookRotation(-transform.forward, Vector3.up);
        transform.rotation = newRot;
        looksLeft = !looksLeft;
    }

    #endregion

    [HideInInspector]
    public string currentAttack;
    [HideInInspector]
    public bool doesDamage;

    public virtual void Attack(int combo) //hier activeer je de aanval
    {
        currentState = State.Attack;
        currentCombo = combo;
        currentAttack = stats.attacks[combo].name;
        anim.SetTrigger(currentAttack);
    }

    public virtual void DoesDamage(bool isTrue) //dit activeer je steeds in de animatie, wanneer die damage doet / niet doet switch je dit dus. 
        //altijd false zetten als combo stopt.
    {
        doesDamage = isTrue;
    }

    public virtual void EndAttack()
    {
        currentState = State.Walk;
        anim.SetTrigger("Idle");
    }

    public virtual void ChangeHP(int addedHP)
    {
        stats.hp += addedHP;

        if (stats.hp < 1)
            Death();
    }

    public virtual void Death()
    {
        anim.SetBool(deathStateName, true);
        Destroy(gameObject);
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
