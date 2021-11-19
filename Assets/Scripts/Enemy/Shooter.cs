using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public GameObject BulletInitLocator;


    [SerializeField] public float RetargetInterval;
    [SerializeField] public float ShootInterval;
    [SerializeField] public float ShootInitSpeed;

    [SerializeField] private UnityEvent<GameObject> OnTargetChanged;
    [SerializeField] private UnityEvent<GameObject> OnShot;

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

            _nextRetargetingTickTime += RetargetInterval;
        }

        if (null != _target && Time.time >= _nextShootingTickTime)
        {
            Shoot();

            _nextShootingTickTime += RetargetInterval;
        }
    }

    public void Retarget()
    {
        GameObject target = null;
        if (_inRangePlayerTracker.InRangePlayers.Any())
        {
            target = _inRangePlayerTracker.GetNearest();
        }

        if (_target == target)
        {
            return;
        }

        _target = target;

        OnTargetChanged.Invoke(_target);
    }

    public void Shoot()
    {
        var bullet = Instantiate(BulletPrefab, BulletInitLocator.transform.position, BulletInitLocator.transform.rotation);
        var bulletMovement = bullet.GetComponent<BulletMovement>();
        bulletMovement.Init(_target, BulletInitLocator.transform.forward, ShootInitSpeed);

        OnShot.Invoke(bullet);
    }
}
