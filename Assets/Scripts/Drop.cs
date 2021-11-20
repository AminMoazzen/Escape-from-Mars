using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drop : MonoBehaviour
{
    [Header("Design Parameters")]
    [SerializeField] private DropType type;

    [SerializeField] private bool destroyOnGround;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent<GameObject> onHit;

    [SerializeField] private UnityEvent onReachGround;

    private void OnTriggerEnter(Collider other)
    {
        DropHitter[] hitters = other.GetComponents<DropHitter>();
        foreach (DropHitter hitter in hitters)
        {
            if (hitter && hitter.Match(type))
            {
                hitter.Hit();
                OnHit(other.gameObject);
            }
        }
    }

    private void OnHit(GameObject hitter)
    {
        onHit.Invoke(hitter);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnHitGround()
    {
        if (destroyOnGround)
            Destroy(gameObject);
        onReachGround.Invoke();
    }
}