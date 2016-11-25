using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text healthPercentage;
    public Image healthOrb;
    public Image staminaBar;
    public int hp;
    public int stamina;

    private GameObject player;

    public void Awake ()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate ()
    {
        healthOrb.fillAmount = (float)player.GetComponent<Stats_Player>().hp / hp;
        staminaBar.fillAmount = (float)player.GetComponent<Stats_Player>().maxStamina / stamina;
    }

}
