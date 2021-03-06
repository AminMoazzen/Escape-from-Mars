using UnityEngine;

public class ContinuousDamager : Damager
{
    [SerializeField] private float _lastDamageTime;

    public void Awake()
    {
        OnObjectHitted.AddListener(OnObjectHittedResetTimes);
        OnObjectInDamageRange.AddListener(OnDamageContinuous);
    }

    private void OnObjectHittedResetTimes(GameObject arg0)
    {
        _lastDamageTime = Time.time;
    }

    public void OnDamageContinuous(GameObject hitObject)
    {
        var health = hitObject.GetComponentInParent<Health>();
        if (null != health)
        {
            var damage = (Time.time - _lastDamageTime) * Damage;

            OnDamage.Invoke(health, damage);

            _lastDamageTime = Time.time;
        }
    }
}
