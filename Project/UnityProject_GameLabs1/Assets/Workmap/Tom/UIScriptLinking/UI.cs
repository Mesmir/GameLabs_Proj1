using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text healthPercentage;
    public Image healthOrb;
    public Image staminaBar;
    public Image[] skillsCD;

    private GameObject player;
    public int health;
    public int stamina;

    public void Awake ()
    {
        player = GameObject.FindWithTag("Player");
        // Everything down here needs to be un-commented.
        //health = player.GetComponent<Stats_Player>().hp;
        //stamina = player.GetComponent<Stats_Player>().stamina;
    }

    void FixedUpdate ()
    {
        healthOrb.fillAmount = (float)health / 1000; // change the 1000 if max health changes.
        staminaBar.fillAmount = (float)stamina / 800; // change the 800 if max stamina changes.
    }

}
