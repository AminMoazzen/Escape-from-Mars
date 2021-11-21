using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementSpeedController : MonoBehaviour
{
    [SerializeField] public DropType AttractiveType;

    [Range(0, 1)][SerializeField] public float OnDropSpeedPercent = 0.5f;

    [SerializeField] private List<GameObject> drops = new List<GameObject>();

    [SerializeField] private NavMeshAgent _navAgent;
    [SerializeField] private float _defaultSpeed;

    public void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _defaultSpeed = _navAgent.speed;
    }

    public void OnEnterDrop(GameObject drop)
    {
        if(!drops.Any(d => d == drop))
        {
            drops.Add(drop);

            _navAgent.speed = _defaultSpeed * OnDropSpeedPercent;
        }        
    }

    public void OnExitFromAllDrops(GameObject drop)
    {
        var removed = drops.Remove(drop);
        if (!drops.Any())
        {
            _navAgent.speed = _defaultSpeed;
        }
    }
}
