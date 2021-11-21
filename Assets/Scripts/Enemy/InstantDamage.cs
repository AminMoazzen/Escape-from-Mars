using UnityEngine;

public class InstantDamage : Damager
{
    public virtual void Awake()
    {
        OnObjectHitted.AddListener(OnObjectHittedInstantDamage);
    }

    private void OnObjectHittedInstantDamage(GameObject other)
    {
        var healthComponent = other.GetComponentInParent<Health>();
        if (null != healthComponent)
        {
            healthComponent.TakeDamage(Damage);
        }
    }
}
