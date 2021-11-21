using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(NavAgentMovement))]
[RequireComponent(typeof(Thrower))]
public class Robot : MonoBehaviour
{
    [Header("References (Required)")]
    [SerializeField] private NavAgentMovement navAgentMovement;

    [SerializeField] private Thrower thrower;

    [Header("Actions (Optional)")]
    [SerializeField] private GameEvent[] activateOn;

    [SerializeField] private GameEvent[] deactivateOn;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent onActivate;

    [SerializeField] private UnityEvent onDeactivate;

    private void Awake()
    {
        foreach (GameEvent gEvent in activateOn)
        {
            gEvent.AddListener(Activate);
        }

        foreach (GameEvent gEvent in deactivateOn)
        {
            gEvent.AddListener(Deactivate);
        }
    }

    public void Activate()
    {
        navAgentMovement.Activate();
        thrower.Activate();
        onActivate.Invoke();
    }

    public void Deactivate()
    {
        navAgentMovement.Deactivate();
        thrower.Deactivate();
        onDeactivate.Invoke();
    }
}