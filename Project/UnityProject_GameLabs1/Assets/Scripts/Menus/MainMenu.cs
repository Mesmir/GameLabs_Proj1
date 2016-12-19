using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int lastSaveFile = 1;
    public string[] levelNames;
    public GameObject loadGame;
    public GameObject credits;

    #region Selected Save Data

    GameHandler.SavedProgress savedProgress;

    #endregion

    public void NewGame()
    {
        GameHandler.inMenu = false;
        SceneManager.LoadScene(levelNames[0], LoadSceneMode.Single);
    }

    #region Load Game

    public void LoadGame(bool setActive)
    {
        //maakt een object in een dropdown menu voor elke save
        loadGame.SetActive(setActive);
    }

    public void LoadSave(int chosenSaveFile)
    {
        //laad save
        GameHandler.inMenu = false;
        SceneManager.LoadScene(levelNames[chosenSaveFile], LoadSceneMode.Single);
    }

    #endregion

    public void Credits(bool setActive)
    {
        credits.SetActive(setActive);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
