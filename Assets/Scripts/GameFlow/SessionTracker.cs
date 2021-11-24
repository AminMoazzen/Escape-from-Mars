using UnityEngine;

public class SessionTracker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ProgressData progressData;

    private void Awake()
    {
        progressData.LoadProgression();
    }

    public void Begin()
    {
        gameManager.LoadLevel();
    }
}