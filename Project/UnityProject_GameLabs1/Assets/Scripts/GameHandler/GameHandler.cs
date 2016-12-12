using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class GameHandler : MonoBehaviour
{
    #region References

    public static bool inMenu;
    public GameObject player;
    [Range(1, 3)]
    public int saveNumber;
    public string fileName;
    [HideInInspector]
    public string folderPath;
    public static SavedProgress savedData;

    #endregion

    #region Checkpoints

    GameObject[] checkpoints;

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        savedData = new SavedProgress();
        SetupReferences();
    }

    private void SetupReferences()
    {
        folderPath = "/SavedDataAssets/" + fileName + saveNumber + ".xml";
        if (!inMenu)
            SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        Checkpoint.gHandler = this;

        LoadProgress();
        Instantiate(player, checkpoints[savedData.checkpoint].transform.position, Quaternion.identity);
        LoadPlayerCombos();
    }

    #region Saving / Loading

    public void LoadProgress()
    {
        if (!File.Exists(Application.dataPath + folderPath))
            SaveProgress();
        XmlSerializer serializer = new XmlSerializer(typeof(SavedProgress));
        FileStream stream = new FileStream(Application.dataPath + folderPath, FileMode.Open);
        savedData = (SavedProgress)serializer.Deserialize(stream) as SavedProgress;
        stream.Close();
    }

    public void LoadPlayerCombos()
    {
        Combat comboRef = player.GetComponent<Combat>();
        foreach (int unlockedCombo in savedData.unlockedCombos)
            comboRef.combos[unlockedCombo].unlocked = true;
    }

    public void SaveProgress()
    {
        savedData.unlockedCombos = new List<int>();
        Combat comboRef = player.GetComponent<Combat>();
        for (int combo = 0; combo < comboRef.combos.Length; combo++)
            if (comboRef.combos[combo].unlocked)
                savedData.unlockedCombos.Add(combo);

        XmlSerializer serializer = new XmlSerializer(typeof(SavedProgress));
        FileStream stream = new FileStream(Application.dataPath + folderPath, FileMode.Create);
        serializer.Serialize(stream, savedData);
        stream.Close();
    }

    #endregion

    #region Objects

    [Serializable]
    public class SavedProgress
    {
        public int level;
        public int checkpoint;
        public List<int> unlockedCombos = new List<int>();
    }

    #endregion
}
