using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] public UnityEvent<GameObject> OnObjectHitted;
    [SerializeField] public UnityEvent<GameObject> OnObjectInDamageRange;


    [SerializeField] public float Damage = 10;
    [SerializeField] public LayerMask objectLayers;

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
        if (objectLayers == (objectLayers | (1 << other.gameObject.layer)))
        {
            var healthComponent = other.GetComponentInParent<Health>();
            if (null != healthComponent)
            {
                _objectOnAttack = other.gameObject;

                OnObjectHitted.Invoke(_objectOnAttack);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == objectLayers.value)
        {
            if (other.gameObject == _objectOnAttack)
            {
                _objectOnAttack = null;
            }
        }
    }
}
