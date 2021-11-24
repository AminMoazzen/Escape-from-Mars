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


    [Header("Gameobject is new trgeted object")]
    [SerializeField] private UnityEvent<GameObject> OnTargetChanged;
    [Header("Gameobject is new trgeted object")]
    [SerializeField] private UnityEvent<GameObject> OnPlayerBeingTargeted;
    [Header("last Gameobject exited from target")]
    [SerializeField] private UnityEvent<GameObject> OnAllPlayerExitedFromTarget;
    [Header("")]
    [SerializeField] private UnityEvent<ShootingIntervalHitedEvent> OnShootingInterval;
    [Header("Gameobject is bullet object")]
    [SerializeField] private UnityEvent<BulletShotEvent> OnShoted;

    private InRangePlayerTracker _inRangePlayerTracker;
    private GameObject _target;
    private float _nextRetargetingTickTime;
    private float _nextShootingTickTime;
    private bool IsStoppedAttacking = false;

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
        }

        if (null != _target && Time.time >= _nextShootingTickTime)
        {
            Shoot();
        }
    }

    public void Retarget()
    {
        _nextRetargetingTickTime = Time.time + RetargetInterval;

        GameObject target = null;
        if (_inRangePlayerTracker.InRangePlayers.Any(p => p.GetComponentInParent<Health>()))
        {
            target = _inRangePlayerTracker.GetNearest();
        }

        if (_target == target)
        {
            return;
        }

        if (null == target)
        {
            OnAllPlayerExitedFromTarget.Invoke(_target);
        }
        else
        {
            if (null == _target)
            {
                OnPlayerBeingTargeted.Invoke(target);
            }
        }

        _target = target;

        OnTargetChanged.Invoke(_target);
    }

    public void Shoot()
    {
        _nextShootingTickTime = Time.time + ShootInterval;

        if (!IsStoppedAttacking)
        {
            if (null != _target)
            {
                var evt = new ShootingIntervalHitedEvent()
                {
                    shooter = this,
                    initTransform = BulletInitLocator.transform,
                    target = _target,
                    prefab = BulletPrefab,
                    initSpeed = ShootInitSpeed
                };

                OnShootingInterval.Invoke(evt);
            }
        }
    }

    public void Shooted()
    {
        var evt = new BulletShotEvent()
        {
            shooter = this,
            initTransform = BulletInitLocator.transform,
            target = _target,
            prefab = BulletPrefab,
            initSpeed = ShootInitSpeed
        };

        OnShoted.Invoke(evt);
    }
}
