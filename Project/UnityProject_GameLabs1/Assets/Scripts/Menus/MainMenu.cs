using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int lastSaveFile = 1;
    public string[] levelNames;
    public GameObject loadGame;

    #region Selected Save Data

    GameHandler.SavedProgress savedProgress;

    #endregion

    public void NewGame()
    {
        GameHandler.inMenu = false;
        SceneManager.LoadScene(levelNames[0], LoadSceneMode.Single);
    }

    public void LoadGame(bool setActive)
    {
        //maak een object in een dropdown menu voor elke save
        loadGame.SetActive(setActive);
    }

    public void LoadSave(int chosenSaveFile)
    {
        //laad save
        GameHandler.inMenu = false;
        SceneManager.LoadScene(levelNames[chosenSaveFile], LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
