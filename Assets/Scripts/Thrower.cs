using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Thrower : MonoBehaviour
{
    [Header("Design Parameters")]
    [SerializeField] private BoolReference freeAiming;

    [SerializeField] private LayerMask aimable;

    [Header("References (Required)")]
    [SerializeField] private InputActionAsset inputAction;

    [SerializeField] private GameObject objectToThrow;

    [Header("Actions (Optional)")]
    [SerializeField] private GameEvent[] activateOn;

    [SerializeField] private GameEvent[] deactivateOn;
    [SerializeField] private GameEvent[] startThrowingOn;
    [SerializeField] private GameEvent[] stopThrowingOn;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent onActivate;

    [SerializeField] private UnityEvent onDeactivate;
    [SerializeField] private UnityEvent onStartThrowing;
    [SerializeField] private UnityEvent onStopThrowing;

    private InputAction _aim;
    private InputAction _fire;
    private Vector3 _direction;
    private bool _isActive;
    private bool _isThrowing;

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

        foreach (GameEvent gEvent in startThrowingOn)
        {
            gEvent.AddListener(StartThrowing);
        }

        foreach (GameEvent gEvent in stopThrowingOn)
        {
            gEvent.AddListener(StopThrowing);
        }
    }

    private void Start()
    {
        Activate();
    }

    public void StartAcceptingInput()
    {
        if (freeAiming.Value)
        {
            if (_aim == null)
                _aim = inputAction.FindAction("Aim", true);
            _aim.Enable();
            _aim.started += Aim;
            _aim.canceled += Aim;
            _aim.performed += Aim;
        }

        if (_fire == null)
            _fire = inputAction.FindAction("Fire", true);
        _fire.Enable();
        _fire.started += Fire;
        _fire.canceled += Fire;
        _fire.performed += Fire;
    }

    public void StopAcceptingInput()
    {
        if (freeAiming.Value)
        {
            _aim.Disable();
            _aim.started -= Aim;
            _aim.canceled -= Aim;
            _aim.performed -= Aim;
        }

        _fire.Disable();
        _fire.started += Fire;
        _fire.canceled += Fire;
        _fire.performed += Fire;
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

    public void StartThrowing()
    {
        _isThrowing = true;
        onStartThrowing.Invoke();
    }

    public void StopThrowing()
    {
        _isThrowing = false;
        onStopThrowing.Invoke();
    }

    private void Update()
    {
        if (_isActive && _isThrowing)
        {
            GameObject go = Instantiate(objectToThrow, transform.position, Quaternion.identity);
            go.transform.DOMove(transform.position + _direction * 5, 1f);
        }
    }

    private void Aim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(input), out hitInfo, 20, aimable))
        {
            Transform thisTransform = transform;
            Vector3 positionInWorld = new Vector3(hitInfo.point.x, thisTransform.position.y, hitInfo.point.z);
            _direction = (positionInWorld - thisTransform.position).normalized;
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        bool isPress = context.ReadValue<float>() > 0;

        if (isPress)
            StartThrowing();
        else
            StopThrowing();
    }
}