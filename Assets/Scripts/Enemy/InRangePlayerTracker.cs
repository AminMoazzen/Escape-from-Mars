using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class InRangePlayerTracker : MonoBehaviour
{
    // For Listener
    //[SerializeField] private GameObjectGameEvent PlayerEnteredOn;
    
    [SerializeField] private UnityEvent<GameObject> OnPlayerEntered;
    [SerializeField] private UnityEvent<GameObject> OnPlayerExited;
    
    private List<GameObject> _inRangePlayers = new List<GameObject>();
    public List<GameObject> InRangePlayers
    {
        get
        {
            _inRangePlayers.RemoveAll(player => null == player);
            return _inRangePlayers;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.GetComponentInParent<Robot>();
        if (isPlayer && !InRangePlayers.Any(p => p.gameObject == other.gameObject))
        {
            InRangePlayers.Add(other.gameObject);


            OnPlayerEntered.Invoke(other.gameObject);            
        }
    }    

    public void OnTriggerExit(Collider other)
    {
        var isPlayer = other.GetComponentInParent<Robot>();
        if (isPlayer && InRangePlayers.Any(p => p.gameObject == other.gameObject))
        {
            InRangePlayers.Remove(other.gameObject);

            OnPlayerExited.Invoke(other.gameObject);            
        }
    }

    public GameObject GetNearest()
    {
        // Remove destroied Objects
        InRangePlayers.RemoveAll(obj => null == obj);

        if (!InRangePlayers.Any())
        {
            return null;
        }

        var minDistance = transform.position.SqrMagnitude2D(InRangePlayers[0].transform.position);
        var minIndex = 0;
        for (var i = 1; i < InRangePlayers.Count - 1; ++i)
        {
            var distance = transform.position.SqrMagnitude2D(InRangePlayers[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        return InRangePlayers[minIndex];
    }
}
