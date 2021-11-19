using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public int MaxHealth = 100;
    [SerializeField] public int CurrentHealth;

    [SerializeField] public UnityEvent<Health, int> OnDamaged;
    [SerializeField] public UnityEvent<GameObject> OnDied;
    [SerializeField] public UnityEvent<GameObject> OnHealthReset;    

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        if(0 == CurrentHealth)
        {
            OnDied.Invoke(gameObject);
        }
        else
        {
            OnDamaged.Invoke(this, amount);
        }
    }

    public void ResetHealth()
    {
        OnHealthReset.Invoke(gameObject);
    }
}
