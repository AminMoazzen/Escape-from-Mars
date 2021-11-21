using UnityEngine;
using UnityEngine.Events;

public class Bullet : InstantDamage
{
    public override void Awake()
    {
        base.Awake();
        OnObjectHitted.AddListener(OnHitObject);
    }

    public void OnHitObject(GameObject hitObject)
    {
        Destroy(gameObject);
    }
}
