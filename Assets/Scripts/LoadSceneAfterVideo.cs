using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadSceneAfterVideo : MonoBehaviour
{
    [SerializeField] private SceneReference nextScene;
    [SerializeField] private VideoPlayer player;

    private void Start()
    {
        player.loopPointReached += (player) => SceneManager.LoadSceneAsync(nextScene.Value.SceneIndex);
    }

    public void Skip()
    {
        SceneManager.LoadSceneAsync(nextScene.Value.SceneIndex);
    }
}