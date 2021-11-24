//using GameAnalyticsSDK;
using ScriptableObjectArchitecture;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//using HomaGames.HomaBelly;

[CreateAssetMenu(fileName = "GameManager.asset", menuName = "New Game Manager", order = 0)]
public class GameManager : ScriptableObject
{
    [SerializeField] private List<SceneReference> levels;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent<int> onLoadedLevel;

    [SerializeField] private UnityEvent onWinLevel;
    [SerializeField] private UnityEvent onLoseLevel;

    private int lastLevel = 0;

    public void WinLevel()
    {
        lastLevel++;

        //LoadLevel();
        onWinLevel.Invoke();
    }

    public void LoseLevel()
    {
        //LoadLevel();
        onLoseLevel.Invoke();
    }

    public void LoadLevel()
    {
        int levelNo = lastLevel;
        int levelInRange = levelNo % levels.Count;

        SceneManager.LoadSceneAsync(levels[levelInRange].Value.SceneIndex).completed += (op) => onLoadedLevel.Invoke(levelNo);
    }
}