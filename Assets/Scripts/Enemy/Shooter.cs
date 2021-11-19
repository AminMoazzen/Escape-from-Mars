using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public GameObject BulletInitLocator;

    [Range(0, 10)] [SerializeField] public float RetargetInterval;
    [Range(0, 10)] [SerializeField] public float ShootInterval;
    [Range(0, 10)] [SerializeField] public float ShootInitSpeed;
    [SerializeField] public bool ShootAfterTargeting = true;

    [SerializeField] private UnityEvent<GameObject> OnTargetChanged;
    [SerializeField] private UnityEvent<GameObject> OnShoted;

    private InRangePlayerTracker _inRangePlayerTracker;
    private GameObject _target;
    private float _nextRetargetingTickTime;
    private float _nextShootingTickTime;

    private void Awake()
    {
        _inRangePlayerTracker = GetComponent<InRangePlayerTracker>();
    }

    private void Start()
    {
        _nextRetargetingTickTime = Time.time;
        _nextShootingTickTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= _nextRetargetingTickTime)
        {
            Retarget();

            _nextRetargetingTickTime = Time.time + RetargetInterval;
        }

        if (null != _target && Time.time >= _nextShootingTickTime)
        {
            Shoot();

            _nextShootingTickTime = Time.time + ShootInterval;
        }
    }

    public void Retarget()
    {
        GameObject target = null;
        if (_inRangePlayerTracker.InRangePlayers.Any(p => p.GetComponentInParent<Health>()))
        {
            target = _inRangePlayerTracker.GetNearest();
        }

        if (_target == target)
        {
            return;
        }

        _target = target;

        if (ShootAfterTargeting)
        {
            Shoot();
        }

        OnTargetChanged.Invoke(_target);
    }

    public void Shoot()
    {
        if (null != _target)
        {
            var bullet = Instantiate(BulletPrefab, BulletInitLocator.transform.position, BulletInitLocator.transform.rotation);
            var bulletMovement = bullet.GetComponent<BulletMovement>();
            bulletMovement.Init(_target, BulletInitLocator.transform.forward, ShootInitSpeed);

            OnShoted.Invoke(bullet);
        }
    }
}
