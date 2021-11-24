using System;
using UnityEngine;

[Serializable]
public class ShootingIntervalHitedEvent
{
    [SerializeField]
    public Shooter shooter;

    [SerializeField]
    public Transform initTransform;

    [SerializeField]
    public GameObject target;

    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    public float initSpeed;
}
