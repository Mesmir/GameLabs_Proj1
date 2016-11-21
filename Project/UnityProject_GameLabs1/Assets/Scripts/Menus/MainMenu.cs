using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public int lastSaveFile = 1;
    public string[] levelNames;

    public void NewGame()
    {
        SceneManager.LoadScene(levelNames[0], LoadSceneMode.Single);
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
