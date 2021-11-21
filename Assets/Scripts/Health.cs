using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public float MaxHealth = 100;
    [SerializeField] public float CurrentHealth;

    [SerializeField] public UnityEvent<Health, float> OnDamaged;
    [SerializeField] public UnityEvent<Health, float> OnHealthChangedNormalized;
    [SerializeField] public UnityEvent<float> OnHealthChangedNormalizedSimple;

    [SerializeField] public UnityEvent<GameObject> OnDied;
    [SerializeField] public UnityEvent<GameObject> OnHealthReset;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)

    {
        CurrentHealth -= amount;

        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        if (0 == CurrentHealth)
        {
            OnDied.Invoke(gameObject);
        }
        else
        {
            OnDamaged.Invoke(this, amount);
        }

        OnHealthChangedNormalized.Invoke(this, CurrentHealth / MaxHealth);
        OnHealthChangedNormalizedSimple.Invoke(CurrentHealth / MaxHealth);
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        OnHealthChangedNormalizedSimple.Invoke(CurrentHealth / MaxHealth);
        OnHealthReset.Invoke(gameObject);
    }
}