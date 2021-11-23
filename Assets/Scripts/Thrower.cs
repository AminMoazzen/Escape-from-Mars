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

    [Range(0, 2)] [SerializeField] private float range = 1;
    [Range(0, 10)] [SerializeField] private float speed = 5;
    [Range(0, 20)] [SerializeField] private float rate = 10;

    [SerializeField] private LayerMask aimable;

    [Header("References (Required)")]
    [SerializeField] private InputActionAsset inputAction;

    [SerializeField] private Drop drop;

    [Header("Actions (Optional)")]
    [SerializeField] private GameEvent[] activateOn;

    [SerializeField] private GameEvent[] deactivateOn;
    [SerializeField] private GameEvent[] startThrowingOn;
    [SerializeField] private GameEvent[] stopThrowingOn;
    [SerializeField] private GameEvent[] freezeOn;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent onActivate;

    [SerializeField] private UnityEvent onDeactivate;
    [SerializeField] private UnityEvent onStartThrowing;
    [SerializeField] private UnityEvent onStopThrowing;
    [SerializeField] private UnityEvent onFreeze;

    private InputAction _aim;
    private InputAction _fire;
    private Vector3 _direction;
    private bool _isActive;
    private float _flyTime;
    private float _instantiateInterval;
    private Coroutine _throwing;

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

        foreach (GameEvent gEvent in freezeOn)
        {
            gEvent.AddListener(StopThrowing);
        }
    }

    private void Start()
    {
        _flyTime = range / speed;
        _instantiateInterval = 1 / rate;
    }

    private void OnEnable()
    {
        StartAcceptingInput();
    }

    private void OnDisable()
    {
        StopAcceptingInput();
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
        if (freeAiming.Value && _aim != null)
        {
            //_aim.Disable();
            _aim.started -= Aim;
            _aim.canceled -= Aim;
            _aim.performed -= Aim;
        }

        if (_fire != null)
        {
            //_fire.Disable();
            _fire.started -= Fire;
            _fire.canceled -= Fire;
            _fire.performed -= Fire;
        }
    }

    public void Activate()
    {
        _isActive = true;
        //StartAcceptingInput();
        onActivate.Invoke();
    }

    public void Deactivate()
    {
        _isActive = false;
        //StopAcceptingInput();
        onDeactivate.Invoke();
    }

    public void StartThrowing()
    {
        if (_throwing == null)
        {
            _throwing = StartCoroutine(Throwing());
            onStartThrowing.Invoke();
        }
    }

    public void StopThrowing()
    {
        if (_throwing != null)
        {
            StopCoroutine(_throwing);
            _throwing = null;
            onStopThrowing.Invoke();
        }
    }

    private IEnumerator Throwing()
    {
        while (true)
        {
            GameObject go = Instantiate(drop.gameObject, transform.position, Quaternion.identity);
            Vector3 destination = transform.position + _direction * range;
            destination.y = 0;
            go.transform.DOMove(destination, _flyTime).onComplete += () => { go.GetComponent<Drop>().OnHitGround(); };
            yield return new WaitForSeconds(_instantiateInterval);
        }
    }

    private void Aim(InputAction.CallbackContext context)
    {
        if (_isActive)
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
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (_isActive)
        {
            bool isPress = context.ReadValue<float>() > 0;

            if (isPress)
                StartThrowing();
            else
                StopThrowing();
        }
    }
}