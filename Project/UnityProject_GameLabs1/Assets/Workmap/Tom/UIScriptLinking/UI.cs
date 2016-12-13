using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    
    public Image healthOrb;
    public Image staminaBar;

    private GameObject player;

    public void Awake ()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate ()
    {
        healthOrb.fillAmount = (float)player.GetComponent<Stats_Player>().hp / 1000;
        staminaBar.fillAmount = (float)player.GetComponent<Stats_Player>().maxStamina / 1000;
    }

}
