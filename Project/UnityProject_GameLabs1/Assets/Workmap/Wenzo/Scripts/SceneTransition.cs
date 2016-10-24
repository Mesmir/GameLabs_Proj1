using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//Don't destroy on load?
public class SceneTransition : MonoBehaviour {
    public List<GameObject> saveList = new List<GameObject>();
    //public List<int> index = new List<int>();
    public GameObject playerGO;
    public GameObject camGO;

    void Start()
    {
        saveList.Add (GameObject.Find("Player"));
        saveList.Add(GameObject.Find("Main Camera"));
        SceneManager.LoadSceneAsync(1);
        //SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        //SceneManager.MoveGameObjectToScene(playerGO, SceneManager.GetSceneAt(1));
        //SceneManager.MoveGameObjectToScene(camGO, SceneManager.GetSceneAt(1));
        //SceneManager.UnloadScene(0);
        //EditorSceneManager.CloseScene(SceneManager.GetSceneAt (0), true);
    }
}
