using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] public UnityEvent<GameObject> OnObjectHitted;
    [SerializeField] public UnityEvent<GameObject> OnObjectInDamageRange;
    [SerializeField] public UnityEvent<GameObject> OnObjectExitedFromDamageRange;
    [SerializeField] public UnityEvent<Health, float> OnDamage;


    [SerializeField] public float Damage = 10;
    [SerializeField] public LayerMask objectLayers;

    [SerializeField] private GameObject _objectOnAttack;
    [SerializeField] private bool _isStoppedAttacking = false;
    [SerializeField]
    public bool IsStoppedAttacking
    {
        get 
        {
            return _isStoppedAttacking;
        }
        protected set
        {
            _isStoppedAttacking = value;
        }
    }

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
        if (objectLayers == (objectLayers | (1 << other.gameObject.layer)))
        {
            if (other.gameObject == _objectOnAttack)
            {
                OnObjectExitedFromDamageRange.Invoke(gameObject);
                _objectOnAttack = null;
            }
        }
    }

    public void StopAttacking()
    {
        IsStoppedAttacking = true;
    }

    public void StartAttacking()
    {
        IsStoppedAttacking = false;
    }
}
