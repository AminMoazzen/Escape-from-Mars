using UnityEngine;

public class ContinuousDamager : Damager
{
    [SerializeField] public float DamageInterval;

    [SerializeField] private float _lastDamageTime;

    public void Awake()
    {
        OnObjectInDamageRange.AddListener(OnDamageRange);
        _lastDamageTime = Time.time;
    }

    public void OnDamageRange(GameObject hitObject)
    {
        if (Time.time >= _lastDamageTime + DamageInterval)
        {
            var health = hitObject.GetComponentInParent<Health>();
            if (null != health)
            {
                health.TakeDamage(Damage);
            }

            _lastDamageTime = Time.time + DamageInterval;
        }
    }
}
