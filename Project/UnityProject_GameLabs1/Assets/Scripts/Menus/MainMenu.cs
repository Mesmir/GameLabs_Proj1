using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public int lastSaveFile = 1;

    public void NewGame()
    {
        //laad eerste scene, simpel als dat
    }

    public void Continue()
    {
        lastSaveFile = PlayerPrefs.GetInt("SaveNumber");
        //laad save
    }

    public void LoadGame()
    {
        //maak een object in een dropdown menu voor elke save
    }

    public void LoadSave(int chosenSaveFile)
    {
        PlayerPrefs.SetInt("SaveNumber", chosenSaveFile);
        //laad save
    }

    public void Exit()
    {

    }
}
