using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _initDirection;
    [SerializeField] private float _initSpeed;

    public void Init(GameObject target, Vector3 initDirection, float initSpeed)
    {
        _target = target;
        _initDirection = initDirection;
        _initSpeed = initSpeed;

        Shooted();
    }

    protected abstract void Shooted();
}
