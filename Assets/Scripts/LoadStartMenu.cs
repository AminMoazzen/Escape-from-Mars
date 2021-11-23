using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartMenu : MonoBehaviour
{
    public void Load()
    {
        SceneManager.LoadSceneAsync(0);
    }
}