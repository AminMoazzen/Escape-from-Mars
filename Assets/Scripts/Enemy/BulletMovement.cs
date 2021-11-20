using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    [SerializeField] protected GameObject _target;
    [SerializeField] protected Vector3 _initDirection;
    [SerializeField] protected float _initSpeed;
    [SerializeField] private Bullet _bullet;

    public void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    public void Init(GameObject target, Vector3 initDirection, float initSpeed)
    {
        _target = target;
        _initDirection = initDirection;
        _initSpeed = initSpeed;

        _bullet.OnObjectHitted.AddListener(ObjectHitedOn);

        Shooted();
    }

    public void ObjectHitedOn(GameObject hitObject)
    {
        Destroy(gameObject);
    }

    protected abstract void Shooted();
}
