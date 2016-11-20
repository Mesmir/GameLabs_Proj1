using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class GameHandler : MonoBehaviour {

    #region References

    public static GameObject player;
    [Range(1, 3)]
    public int saveNumber;
    public string fileName;
    [HideInInspector]
    public string folderPath;
    public static SavedProgress savedData;

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SetupReferences(); //moet nog mee verder
    }

    public void OnExitToMainMenu()
    {
        Destroy(gameObject);
    }

    private void SetupReferences()
    {
        StartCoroutine(FindPlayer("Player"));
        saveNumber = PlayerPrefs.GetInt("SaveNumber");
        folderPath = "/SavedDataAssets/" + fileName + saveNumber + ".xml";
    }

    #region Saving / Loading

    public void LoadProgress()
    {
        bool excistingSaveFile = false;
        if (!File.Exists(Application.dataPath + folderPath))
            SaveProgress();
        else
            excistingSaveFile = true;
        XmlSerializer serializer = new XmlSerializer(typeof(SavedProgress));
        FileStream stream = new FileStream(Application.dataPath + folderPath, FileMode.Open); //wel een xml bestand klaarzetten
        savedData = (SavedProgress)serializer.Deserialize(stream) as SavedProgress;
        stream.Close();

        if (excistingSaveFile)
        {
            Combat comboRef = player.GetComponent<Combat>();
            foreach (int unlockedCombo in savedData.unlockedCombos)
                comboRef.combos[unlockedCombo].unlocked = true;
        }
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

    private IEnumerator FindPlayer(string tag)
    {
        GameObject[] matches = new GameObject[0];
        while (matches.Length == 0) {
            matches = GameObject.FindGameObjectsWithTag(tag);
            yield return false;
        }
        player = matches[0];
        LoadProgress();
    }

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
