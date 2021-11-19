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


    [SerializeField] string PlayerTag = GameTags.Player;
    
    public List<GameObject> InRangePlayers = new List<GameObject>();

    public void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag(PlayerTag);
        if (isPlayer && !InRangePlayers.Any(p => p.gameObject == other.gameObject))
        {
            InRangePlayers.Add(other.gameObject);


            OnPlayerEntered.Invoke(other.gameObject);            
        }
    }    

    public void OnTriggerExit(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag(PlayerTag);
        if (isPlayer && InRangePlayers.Any(p => p.gameObject == other.gameObject))
        {
            InRangePlayers.Remove(other.gameObject);

            OnPlayerExited.Invoke(other.gameObject);            
        }
    }

    public GameObject GetNearest()
    {
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
