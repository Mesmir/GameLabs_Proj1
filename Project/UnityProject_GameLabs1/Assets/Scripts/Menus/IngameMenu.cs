using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//Edit's Renzo; Using Scenemanagement en QuitToMenu aangevuld;

[RequireComponent(typeof(GameHandler))]
public class IngameMenu : MonoBehaviour {

    public GameMenu menu;
    private GameHandler handler;

    #region InspectorSettings

    public string pauseButton;

    #endregion

    private void Start()
    {
        handler = GetComponent<GameHandler>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(pauseButton))
        {
            bool isActive = (menu.thisMenu.activeSelf) ? false : true;
            if (isActive)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
            menu.thisMenu.SetActive(isActive);
        }
    }

    public void SwitchMenu(bool intoMenu) //dit kan ook op de back knop
    {
        foreach (Transform child in menu.thisMenu.transform)
            if (menu.partOfMenu.Contains(child))
                child.gameObject.SetActive(intoMenu);
            else
                child.gameObject.SetActive(!intoMenu);
    }

    public void Continue()
    {
        menu.thisMenu.SetActive(false);
    }

    public void CheckCombos(Transform comboMenu)
    {
        SwitchMenu(false);
        comboMenu.gameObject.SetActive(true);

        #region ShowCombos

        Combat combat = handler.player.GetComponent<Combat>();
        for (int x = 0; x < combat.combos.Length; x++)
        {
            string thisCombo = combat.combos[x].name + ": ";
            for (int y = 0; y < combat.combos[x].comboString.Length; y++)
                thisCombo += combat.combos[x].comboString[y].ToString() + ", ";
            Text combo = comboMenu.GetChild(x).GetComponent<Text>();
            //show alle combo's, maak de niet unlocked combo's red, en unlocked groen, en ablities natuurlijk
            combo.color = (combat.combos[x].unlocked) ? Color.green : Color.red;
            combo.text = thisCombo;
        }

        #endregion
    }

    public void QuitToMenu(bool saving)
    {
        if (saving)
            handler.SaveProgress();
        //laad menu scene
        menu.thisMenu.SetActive(false);
        SceneManager.LoadSceneAsync(0);
        //GameObject.Find("Canvas").SetActive(false); //temoporary ugly solution
    }

    public void QuitToDesktop(bool saving)
    {
        if (saving)
            handler.SaveProgress();
        Application.Quit();
    }

    [Serializable]
    public class GameMenu
    {
        public GameObject thisMenu;
        public List<Transform> partOfMenu = new List<Transform>();
    }
}
