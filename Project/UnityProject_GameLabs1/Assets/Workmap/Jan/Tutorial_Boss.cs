using UnityEngine;
using System;
using System.Collections;
using Enemy;

//enable dit script met een trigger, tot die tijd zit ie gewoon niks te doen
public class Tutorial_Boss : Enemy_Base {

    public Enemy_Class.Enemy thisCharacter;
    public MonologueOnDeath monologue;
    public int neededSkillPlayerInList;
    public float attackFrequency;
    public float changeMovementFrequency;
    private Vector3 startPos, startRot;
    private bool movingLeft;
    private GameObject player;
    private bool canAttack = true;
    public LayerMask boundsArea;

    void Awake()
    {
        player = FindPlayer();
        DontDestroyOnLoad(transform.gameObject);
        startPos = transform.position;
        startRot = transform.eulerAngles;
    }

    private bool LookForWalls(int dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.left * dir, out hit, changeMovementFrequency + 0.5f, boundsArea))
        {
            return true;
        }
        return false;
    }

    void OnLevelWasLoaded()
    {
        FindPlayer();
        transform.position = startPos;
        transform.eulerAngles = startRot;
    }

    void Start ()
    {
        StartBattle();
        InvokeRepeating("CheckAttacks", 2, attackFrequency);
        InvokeRepeating("ChangeDirectionMovement", 2, changeMovementFrequency);
    }
	
	void Update ()
    {
        Movement();
	}

    private void Movement()
    {
        if (canAttack == false)
            return;
        int dir = (movingLeft) ? 1 : -1;
        bool obstacleSpotted;
        obstacleSpotted = LookForWalls(dir);
        if (obstacleSpotted == false) {
            obstacleSpotted = LookForWalls(-dir);
        }
        transform.Translate(Vector3.left * dir * thisCharacter.speed * Time.deltaTime);
    }

    private void ChangeDirectionMovement()
    {
        movingLeft = (UnityEngine.Random.Range(0, 2) == 1) ? true : false;
    }

    public void StartBattle()
    {
        //als de speler de dash wel niet heeft, spreek dan
        if (player.GetComponent<Player_Script>().combos[neededSkillPlayerInList].unlocked)
        {
            Debug.Log(monologue.monologueWithDash[monologue.deathsWithDash]);
        }
        else
        {
            Debug.Log(monologue.monologueWithoutDash[monologue.deathsWithoutDash]);
            if (monologue.deathsWithoutDash + 1 == monologue.monologueWithoutDash.Length)
                Application.Quit();
        }
    }

    protected override void OnDeath()
    {
        Debug.Log(monologue.onDeath);
        base.OnDeath();
    }

    private void CheckAttacks()
    {
        if (canAttack)
        {
            float distancePlayer = CheckLocationPlayer();
            if (CheckUniqueAttacks(distancePlayer))
                return;
            foreach (Enemy_Class.EnemyAttack attack in thisCharacter.attacks)
            {
                if (distancePlayer > attack.minRange && distancePlayer < attack.maxRange)
                {
                    Debug.Log(attack.name);
                    canAttack = false;
                    Attack(attack);
                    return;
                }
            }
        }
        return;
    }

    private void Attack(Enemy_Class.EnemyAttack attack)
    {
        //wordt aan gewerkt
        canAttack = true;
    }

    private bool CheckUniqueAttacks(float dis)
    {
        //wordt misschien nog gebruikt, als er genoeg animaties zijn
        return false;
    }

    private float CheckLocationPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    protected override void KilledPlayer()
    {
        //als bepaalde ability unlocked == false dan deathWithoutDash++
        base.KilledPlayer();
    }

    public void Damaged(int damage) //deze roep je aan als je damage krijgt
    {
        if(ReceiveDamage(damage, thisCharacter.hp))
            OnDeath();
    }

    protected override bool ReceiveDamage(int damage, int remainingHP)
    {
        if(base.ReceiveDamage(damage, remainingHP))
        {
            return true;
        }
        return false;
    }

    [Serializable]
    public class MonologueOnDeath {
        [HideInInspector]
        public int deathsWithoutDash, deathsWithDash;

        public string[] monologueWithoutDash;
        public string[] monologueWithDash;
        public string onDeath;

        MonologueOnDeath(string[] _monologueWithoutDash, string[] _monologueWithDash, string _onDeath)
        {
            monologueWithDash = _monologueWithDash;
            monologueWithoutDash = _monologueWithoutDash;
            onDeath = _onDeath;
        }

        MonologueOnDeath(bool hadDashSkill)
        {
            if (hadDashSkill) {
                if(deathsWithDash + 1 < monologueWithDash.Length)
                    deathsWithDash++;
            }
            else
            {
                if (deathsWithDash + 1 < monologueWithoutDash.Length)
                    deathsWithoutDash++;
            }
        }
    }
}
