using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public RecordData recordData;
    public RecordData playerRecord;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        recordData = new RecordData("", 0);
        playerRecord = new RecordData("", 0);
        DontDestroyOnLoad(gameObject);
        LoadRecord();
    }

    // Ref: https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
    public void SaveRecord()
    {
        string fileRecord = Application.persistentDataPath + "/RecordHolder.json";
        string json = JsonUtility.ToJson(recordData);
        File.WriteAllText(fileRecord, json);
    }

    public void LoadRecord()
    {
        string fileRecord = Application.persistentDataPath + "/RecordHolder.json";
        Debug.Log("LoadRecord fileRecord=" + fileRecord);
        if (File.Exists(fileRecord))
        {
            string json = File.ReadAllText(fileRecord);
            RecordData rd = JsonUtility.FromJson<RecordData>(json);
            recordData = rd;
        }
    }
}

[System.Serializable]
public class RecordData
{
    public string BestName;
    public int BestScore;

    public RecordData(string bestName, int bestScore)
    {
        BestName = bestName;
        BestScore = bestScore;
    }
}
