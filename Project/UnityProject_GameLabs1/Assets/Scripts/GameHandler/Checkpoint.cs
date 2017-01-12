using UnityEngine;
using System;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public CheckPointData thisCheckPoint;
    public static GameHandler gHandler;
    [Header("Zet de particle op de torch zelf, en zet hem disabled, dit script zet hem aan.")]
    public GameObject torch;

    private void OnTriggerEnter(Collider x)
    {
        if (x.transform.tag == "Player")
        {
            SaveProgress();
            LightTorch();
        }
    }

    private bool lit = false;
    private void LightTorch()
    {
        if (lit)
            return;
        torch.SetActive(true);
    }

    private void SaveProgress()
    {
        GameHandler.savedData.level = thisCheckPoint.checkPoint;
        GameHandler.savedData.level = thisCheckPoint.level;
        gHandler.SaveProgress();
    }

    [Serializable]
    public class CheckPointData
    {
        public int level = 1;
        public int checkPoint = 1;
    }
}
