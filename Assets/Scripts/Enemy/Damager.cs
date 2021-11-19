using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] public int Damage;
    [SerializeField] public string ObjectTag = GameTags.Player;

    [SerializeField] public UnityEvent<GameObject> OnObjectHitted;

    public void OnTriggerEnter(Collider other)
    {
        var healthComponent = other.GetComponentInParent<Health>();
        if (null != healthComponent)
        {
            healthComponent.TakeDamage(Damage);

            OnObjectHitted.Invoke(other.gameObject);

            Destroy(gameObject, 1);
        }
    }
}
