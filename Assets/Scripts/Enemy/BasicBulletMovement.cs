using UnityEngine;
using UnityEngine.Events;

public class BasicBulletMovement : BulletMovement
{
    [SerializeField] public UnityEvent<Vector3> OnBulletDistroied;

    [Range(0, 60)][SerializeField] public float LifeTime = 5;
    [SerializeField] private bool _shooted = true;

    protected override void Shooted()
    {
        _shooted = true;
    }

    public void OnDestroy()
    {
        OnBulletDistroied.Invoke(transform.position);
    }

    public void Update()
    {
        if(_shooted)
        {
            transform.position += _initDirection * _initSpeed * Time.deltaTime;
            LifeTime -= Time.deltaTime;
            if(LifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
