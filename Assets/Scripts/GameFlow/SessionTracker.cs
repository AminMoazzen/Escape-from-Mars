using UnityEngine;

public class SessionTracker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ProgressData progressData;

    public void Begin()
    {
        //DontDestroyOnLoad(gameObject);
        progressData.LoadProgression();
        gameManager.LoadLevel();
    }
}