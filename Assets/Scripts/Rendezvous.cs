using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Rendezvous : MonoBehaviour
{
    [Range(1, 3)] [SerializeField] private int minBots = 1;

    [SerializeField] private UnityEvent onBotArrived;
    [SerializeField] private UnityEvent onLastBotArrived;

    private int _remainingBots;

    private void Start()
    {
        _remainingBots = minBots;
    }

    private void OnTriggerEnter(Collider other)
    {
        Robot bot = other.GetComponent<Robot>();

        if (bot)
        {
            _remainingBots--;

            if (_remainingBots == 0)
                onLastBotArrived.Invoke();
            else
                onBotArrived.Invoke();
        }
    }
}