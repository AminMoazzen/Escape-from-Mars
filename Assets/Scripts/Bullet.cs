using UnityEngine;
using UnityEngine.Events;

public class Bullet : Damager
{
    public void Awake()
    {
        OnObjectHitted.AddListener(OnHitObject);
    }

    public void OnHitObject(GameObject hitObject)
    {
        Destroy(gameObject);
    }
}
