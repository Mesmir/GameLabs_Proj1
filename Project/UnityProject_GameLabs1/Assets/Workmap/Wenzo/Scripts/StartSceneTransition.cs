using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//DontDestroyOnLoad();
public class StartSceneTransition : MonoBehaviour {

    public GameObject sceneSwapper;
    public List<GameObject> saveList = new List<GameObject>();
    int sceneNumber;

    void Start()
    {
        saveList.Add(GameObject.Find("Player"));
        saveList.Add(GameObject.Find("Main Camera"));
        saveList.Add(GameObject.Find("LeftBorder"));
        saveList.Add(GameObject.Find("RightBorder"));

        for (int i = 0; i < saveList.Count; i++)
        {
            DontDestroyOnLoad(saveList[i]);
        }
        print(SceneManager.sceneCount);
    }

    void OnTriggerEnter()
    {
        sceneNumber += 1;
        SceneManager.LoadSceneAsync(sceneNumber);
        Vector3 playerPos = saveList[0].transform.position;
        playerPos.x = 0;
        saveList[0].transform.position = playerPos;
        Vector3 camPos = saveList[1].transform.position;
        camPos.x = 0;
        saveList[1].transform.position = camPos;
    }
}
