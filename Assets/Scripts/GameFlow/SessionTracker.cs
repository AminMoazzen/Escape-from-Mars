using UnityEngine;

public class SessionTracker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ProgressData progressData;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        progressData.LoadProgression();
        gameManager.LoadLevel();
    }
}