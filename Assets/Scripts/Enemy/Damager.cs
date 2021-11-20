using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] public UnityEvent<GameObject> OnObjectHitted;
    [SerializeField] public UnityEvent<GameObject> OnObjectInDamageRange;


    [SerializeField] public float Damage = 10;
    [SerializeField] public string ObjectTag = GameTags.Player;

    [SerializeField] private GameObject _objectOnAttack;

    public void Update()
    {
        if(null != _objectOnAttack)
        {
            OnObjectInDamageRange.Invoke(_objectOnAttack);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var healthComponent = other.GetComponentInParent<Health>();
        if (null != healthComponent)
        {
            _objectOnAttack = other.gameObject;

            healthComponent.TakeDamage(Damage);

            HitObject(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == _objectOnAttack)
        {
            _objectOnAttack = null;
        }
    }

    public void HitObject(GameObject hittedObject)
    {
        if (null != hittedObject)
        {
            OnObjectHitted.Invoke(hittedObject);
        }
    }
}
