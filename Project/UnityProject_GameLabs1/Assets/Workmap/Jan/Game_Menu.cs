using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_Menu : MonoBehaviour {

    public Transform menu;
    public Transform comboMenu;

    public Transform menuButtons; //parent van alle menu buttons buiten back, sinds die alleen verschijnt als je terug moet
    private Transform openMenu;
    public Transform backButton;

    public KeyCode pauseButton;
    private GameObject player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menu.gameObject.SetActive(false);
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            bool isActive = (menu.gameObject.activeSelf) ? false : true;
            if (isActive)
            {
                Time.timeScale = 0;
            }
            else
            {
                Back();
                Time.timeScale = 1;
            }
            menu.gameObject.SetActive(isActive);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        Back();
        menu.gameObject.SetActive(false);
    }

    public void CheckCombos(Transform nextMenu)
    {
        Player_Script ps = player.GetComponent<Player_Script>();
        EnableFollowupMenu(nextMenu);
        for (int x = 0; x < ps.combos.Length; x++)
        {
            string thisCombo = ps.combos[x].name + ": ";
            for (int y = 0; y < ps.combos[x].comboString.Length; y++)
            {
                thisCombo += ps.combos[x].comboString[y].ToString() + ", ";
            }
            Text combo = comboMenu.GetChild(x).GetComponent<Text>();
            //show alle combo's, maak de niet unlocked combo's red, en unlocked groen, en ablities natuurlijk
            combo.color = (ps.combos[x].unlocked) ? Color.green : Color.red;
            combo.text = thisCombo;
        }
    }

    public void QuitToMenu()
    {
        //laad menu scene
    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void Back()
    {
        menuButtons.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        if (openMenu != null) {
            openMenu.gameObject.SetActive(false);
            openMenu = null;
        }
    }

    private void EnableFollowupMenu(Transform newMenu)
    {
        menuButtons.gameObject.SetActive(false);
        openMenu = newMenu;
        openMenu.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
}
