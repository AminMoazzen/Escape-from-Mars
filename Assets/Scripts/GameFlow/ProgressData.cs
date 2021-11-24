//using EasyButtons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ProgressData.asset", menuName = "New Progress Data", order = 0)]
public class ProgressData : ScriptableObject
{
    private int _lastLevel;
    private string _saveFilePath;

    public int lastLevel
    { set { _lastLevel = value; } get { return _lastLevel; } }

    public void SetSaveFilePath()
    {
        if (!string.IsNullOrEmpty(_saveFilePath))
            return;

        _saveFilePath = Application.persistentDataPath + "/" + Application.productName.Replace(' ', '_') + ".sav";

        Debug.Log("Set save file path to: " + _saveFilePath);
    }

    public void SaveProgression()
    {
        SetSaveFilePath();
        string saveJson = JsonConvert.SerializeObject(lastLevel);
        File.WriteAllText(_saveFilePath, saveJson);
    }

    //[Button]
    public void LoadProgression()
    {
        SetSaveFilePath();

        string saveJson;
        try
        {
            saveJson = File.ReadAllText(_saveFilePath);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return;
        }

        _lastLevel = JsonConvert.DeserializeObject<int>(saveJson);
    }

    //[Button]
    public void DeleteProgression()
    {
        SetSaveFilePath();
        File.Delete(_saveFilePath);
        _lastLevel = 0;
    }
}