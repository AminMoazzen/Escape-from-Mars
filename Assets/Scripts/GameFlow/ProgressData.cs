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
    [Serializable]
    private struct SaveData
    {
        //public bool[] hasWonLevel;
        public Dictionary<string, int> intParameters;

        public Dictionary<string, long> dateTimeParameters;

        public SaveData(SaveData data)
        {
            //hasWonLevel = data.hasWonLevel;
            intParameters = data.intParameters;
            dateTimeParameters = data.dateTimeParameters;
        }

        public void Reset()
        {
            //hasWonLevel = new bool[SceneManager.sceneCountInBuildSettings];
            intParameters = new Dictionary<string, int>();
            dateTimeParameters = new Dictionary<string, long>();
        }
    }

    private SaveData _saveData;
    private string _saveFilePath;

    public int coins => _saveData.intParameters["coins"];
    public int buildBlocks => _saveData.intParameters["builtBlocks"];

    public int GetIntParameter(string parameter)
    {
        if (!_saveData.intParameters.ContainsKey(parameter))
            _saveData.intParameters.Add(parameter, 0);

        return _saveData.intParameters[parameter];
    }

    public void SetIntParameter(string parameter, int amount)
    {
        if (_saveData.intParameters.ContainsKey(parameter))
            _saveData.intParameters[parameter] = amount;
        else
            _saveData.intParameters.Add(parameter, amount);
    }

    public void IncreaseIntParameter(string parameter, int amount)
    {
        if (_saveData.intParameters.ContainsKey(parameter))
            _saveData.intParameters[parameter] += amount;
        else
            _saveData.intParameters.Add(parameter, amount);
    }

    public DateTime GetDateTimeParameter(string parameter)
    {
        if (!_saveData.dateTimeParameters.ContainsKey(parameter))
            _saveData.dateTimeParameters.Add(parameter, 0);

        return DateTime.FromFileTimeUtc(_saveData.dateTimeParameters[parameter]);
    }

    public void SetDateTimeParameter(string parameter, DateTime amount)
    {
        if (_saveData.dateTimeParameters.ContainsKey(parameter))
            _saveData.dateTimeParameters[parameter] = amount.ToFileTimeUtc();
        else
            _saveData.dateTimeParameters.Add(parameter, amount.ToFileTimeUtc());
    }

    public void SetWinState(int sceneIndex, bool hasWon)
    {
        //_saveData.hasWonLevel[sceneIndex] = hasWon;
    }

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
        string saveJson = JsonConvert.SerializeObject(_saveData);
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
            _saveData.Reset();
            Debug.LogWarning(e.Message);
            return;
        }

        SaveData data = JsonConvert.DeserializeObject<SaveData>(saveJson);
        _saveData = new SaveData(data);
    }

    //[Button]
    public void DeleteProgression()
    {
        SetSaveFilePath();
        File.Delete(_saveFilePath);
        _saveData.Reset();
    }
}