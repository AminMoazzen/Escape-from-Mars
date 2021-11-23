using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drop : MonoBehaviour
{
    [Header("Design Parameters")]
    [SerializeField] private DropType type;

    [SerializeField] private bool destroyOnGround;
    [SerializeField] private float dieDelay;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent<GameObject> onHit;

    [SerializeField] private UnityEvent<GameObject> OnExitedFromDrop;
    [SerializeField] private UnityEvent onSpawn;
    [SerializeField] private UnityEvent onDie;

    [SerializeField] private UnityEvent onReachGround;

    private void Start()
    {
        onSpawn.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        DropHitter[] hitters = other.GetComponents<DropHitter>();
        foreach (DropHitter hitter in hitters)
        {
            if (hitter && hitter.Match(type))
            {
                hitter.Hit();

                hitter.EnterToDrop(gameObject);

                onHit.Invoke(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DropHitter[] hitters = other.GetComponents<DropHitter>();
        foreach (DropHitter hitter in hitters)
        {
            if (hitter && hitter.Match(type))
            {
                hitter.ExitedFromDrop(gameObject);

                OnExitedFromDrop.Invoke(other.gameObject);
            }
        }
    }

    public void Die()
    {
        onDie.Invoke();
        Destroy(gameObject, dieDelay);
    }

    public void OnHitGround()
    {
        if (destroyOnGround)
            Destroy(gameObject);
        onReachGround.Invoke();
    }
}