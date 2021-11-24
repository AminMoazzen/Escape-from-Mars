using UnityEngine;

public class OnDieDestroyer : MonoBehaviour
{
    [SerializeField] private float delayBeforeDestroy = 0.2f;

    public void DestroyOnDie(GameObject gameObject)
    {
        Destroy(gameObject, delayBeforeDestroy);
    }
}