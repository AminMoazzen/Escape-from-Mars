using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    [SerializeField] protected GameObject _target;
    [SerializeField] protected Vector3 _initDirection;
    [SerializeField] protected float _initSpeed;

    public void Init(GameObject target, Vector3 initDirection, float initSpeed)
    {
        _target = target;
        _initDirection = initDirection;
        _initSpeed = initSpeed;

        Shooted();
    }

    protected abstract void Shooted();
}
