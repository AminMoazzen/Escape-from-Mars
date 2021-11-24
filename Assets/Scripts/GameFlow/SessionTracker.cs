using UnityEngine;

public class SessionTracker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void Begin()
    {
        gameManager.LoadLevel();
    }
}