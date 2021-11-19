using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentMovement : MonoBehaviour
{
    [Header("Design Parameters")]
    [SerializeField] private FloatReference speed;

    [Header("References (Required)")]
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private InputActionAsset inputAction;

    [SerializeField] private Vector3Reference position;

    [Header("Actions (Optional)")]
    [SerializeField] private GameEvent[] activateOn;

    [SerializeField] private GameEvent[] deactivateOn;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent onActivate;

    [SerializeField] private UnityEvent onDeactivate;

    private InputAction _move;
    private Vector3 _direction;
    private bool _isActive;

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

        position.Value = transform.position;
    }

    public void StartAcceptingInput()
    {
        if (_move == null)
            _move = inputAction.FindAction("Move", true);
        _move.Enable();
        _move.started += Move;
        _move.canceled += Move;
        _move.performed += Move;
    }

    public void StopAcceptingInput()
    {
        if (_move != null)
        {
            //_move.Disable();
            _move.started -= Move;
            _move.canceled -= Move;
            _move.performed -= Move;
        }
    }

    public void Activate()
    {
        _isActive = true;
        StartAcceptingInput();
        onActivate.Invoke();
    }

    public void Deactivate()
    {
        _isActive = false;
        StopAcceptingInput();
        onDeactivate.Invoke();
    }

    private void Update()
    {
        if (_isActive)
        {
            agent.Move(_direction * (speed.Value * Time.deltaTime));
            position.Value = transform.position;
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _direction.x = input.x;
        _direction.z = input.y;
        _direction.y = 0;
    }
}