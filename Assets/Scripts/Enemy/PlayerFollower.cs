using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(InRangePlayerTracker))]
public class PlayerFollower : MonoBehaviour
{
    [SerializeField] public float RetargetInterval;
    [SerializeField] public bool NeverUnlock;

    [SerializeField] private UnityEvent<GameObject> OnTargetChanged;
    [SerializeField] private UnityEvent<GameObject> OnStartedFollowPlayer;
    [SerializeField] private UnityEvent<GameObject> OnStopedFollowPlayer;

    private NavMeshAgent _navMeshAgent;
    private InRangePlayerTracker _inRangePlayerTracker;
    private GameObject _target;
    private float _nextTickTime;

    private void Awake()
    {
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        _inRangePlayerTracker = GetComponent<InRangePlayerTracker>();

        if(null == _navMeshAgent)
        {
            Debug.LogError($"Parent does not contain {nameof(NavMeshAgent)} component", transform.parent);
        }
    }

    private void Start()
    {
        _nextTickTime = Time.time;
    }

    private void Update()
    {
        if(Time.time >= _nextTickTime)
        {
            Retarget();

            UpdateAgentDestination();            

            _nextTickTime += RetargetInterval;
        }
    }

    public void Retarget()
    {
        if(NeverUnlock && null != _target)
        {
            return;
        }

        GameObject target = null;
        if(_inRangePlayerTracker.InRangePlayers.Any())
        {
            target = _inRangePlayerTracker.GetNearest();
        }

        if (_target == target)
        {
            return;
        }
        else
        {
            if(null == target)
            {
                OnStopedFollowPlayer.Invoke(_target);
            }
            else
            {
                OnStartedFollowPlayer.Invoke(target);
            }
        }

        
        _target = target;

        OnTargetChanged.Invoke(_target);
    }

    private void UpdateAgentDestination()
    {
        if (null == _target)
        {
            _navMeshAgent.isStopped = true;
            return;
        }

        _navMeshAgent.SetDestination(_target.transform.position);
        _navMeshAgent.isStopped = false;
    }
}
