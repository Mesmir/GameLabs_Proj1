using UnityEngine;
using System.Collections;

public class Stats_Player_Old : MonoBehaviour {

    public enum StaminaLevels { Frenzy = 0, Weak = 100, Normal = 300, Strong = 800};
    public int FrenzyLock, WeakLock, NormalLock, StrongLock; //de combos die je lockt als je sterker/zwakker wordt
    [Range(0,1000)]
    public int hp = 1000;
    [Range(0, 1000)]
    public int stamina = (int)StaminaLevels.Normal; //geef aan de interface een bar stamina mee, en een string staminalevel
    public int lossStaminaPerSec = 2;
    private GameObject player;
    private Player_Script reference;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reference = player.GetComponent<Player_Script>();
        InvokeTick();
    }

    public void InvokeTick()
    {
        InvokeRepeating("TickStamina", 0, 1F);
    }
	
    private void TickStamina()
    {
        stamina -= lossStaminaPerSec;
        CheckStamina();
    }

    private void CheckStamina()
    {
        if (stamina < (int)StaminaLevels.Frenzy)
        {
            stamina = 0;
            //start draining life
            ChangeHealth(-1);
            reference.combos[FrenzyLock].unlocked = reference.combos[WeakLock].unlocked = reference.combos[NormalLock].unlocked = reference.combos[StrongLock].unlocked = false;
        }
        else if (stamina < (int)StaminaLevels.Weak)
        {
            //lock bepaalde attacks
            //unlock frenzy mode
            reference.combos[FrenzyLock].unlocked = true;
            reference.combos[WeakLock].unlocked = reference.combos[NormalLock].unlocked = reference.combos[StrongLock].unlocked = false;
        }
        else if (stamina < (int)StaminaLevels.Normal)
        {
            //lock bepaalde attacks
            reference.combos[FrenzyLock].unlocked = reference.combos[WeakLock].unlocked = true;
            reference.combos[NormalLock].unlocked = reference.combos[StrongLock].unlocked = false;
            //unlock bepaalde andere
        }
        else if (stamina < (int)StaminaLevels.Strong)
        {
            //lock bepaalde attacks
            reference.combos[WeakLock].unlocked = reference.combos[NormalLock].unlocked = true;
            reference.combos[FrenzyLock].unlocked = reference.combos[StrongLock].unlocked = false;
            //unlock bepaalde andere
        }
        else
        {
            reference.combos[WeakLock].unlocked = reference.combos[NormalLock].unlocked = reference.combos[StrongLock].unlocked = true;
            reference.combos[FrenzyLock].unlocked = false;
            //unlock alle skills behalve frenzy
        }
    }

    private void CheckHealth()
    {
        if(hp < 1)
        {
            //dead
        }
    }

    public void ChangeStamina(int excaustion)
    {
        stamina += excaustion;
        CheckStamina();
    }

    public void ChangeHealth(int damage)
    {
        hp += damage;
        CheckHealth();
    }
}