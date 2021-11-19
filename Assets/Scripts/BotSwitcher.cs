using Cinemachine;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BotSwitcher : MonoBehaviour
{
    [Header("References (Required)")]
    [SerializeField] private InputActionAsset inputAction;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private Robot oiler;
    [SerializeField] private Robot flamer;
    [SerializeField] private Robot icer;

    [Header("Actions (Optional)")]
    [SerializeField] private IntGameEvent[] switchOn;

    [Header("Events (Optional)")]
    [SerializeField] private UnityEvent<int> onSwitch;

    private InputAction _switchToOiler;
    private InputAction _switchToFlamer;
    private InputAction _switchToIcer;

    private void Awake()
    {
        foreach (IntGameEvent gEvent in switchOn)
        {
            gEvent.AddListener(SwitchTo);
        }

        _switchToOiler = inputAction.FindAction("Switch to Oiler", true);
        _switchToOiler.Enable();
        _switchToOiler.started += SwitchToOiler;
        _switchToOiler.canceled += SwitchToOiler;
        _switchToOiler.performed += SwitchToOiler;

        _switchToFlamer = inputAction.FindAction("Switch to Flamer", true);
        _switchToFlamer.Enable();
        _switchToFlamer.started += SwitchToFlamer;
        _switchToFlamer.canceled += SwitchToFlamer;
        _switchToFlamer.performed += SwitchToFlamer;

        _switchToIcer = inputAction.FindAction("Switch to Icer", true);
        _switchToIcer.Enable();
        _switchToIcer.started += SwitchToIcer;
        _switchToIcer.canceled += SwitchToIcer;
        _switchToIcer.performed += SwitchToIcer;
    }

    private void Start()
    {
        SwitchTo(0);
    }

    public void SwitchTo(int botIndex)
    {
        switch (botIndex)
        {
            case 0:
                oiler.Activate();
                flamer.Deactivate();
                icer.Deactivate();
                virtualCamera.Follow = oiler.transform;
                break;

            case 1:
                oiler.Deactivate();
                flamer.Activate();
                icer.Deactivate();
                virtualCamera.Follow = flamer.transform;
                break;

            case 2:
                oiler.Deactivate();
                flamer.Deactivate();
                icer.Activate();
                virtualCamera.Follow = icer.transform;
                break;
        }

        onSwitch.Invoke(botIndex);
    }

    private void SwitchToOiler(InputAction.CallbackContext context)
    {
        SwitchTo(0);
    }

    private void SwitchToFlamer(InputAction.CallbackContext context)
    {
        SwitchTo(1);
    }

    private void SwitchToIcer(InputAction.CallbackContext context)
    {
        SwitchTo(2);
    }
}