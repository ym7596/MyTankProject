using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TabState
{
    None,
    Start,
    Hold,
    Canceld,
}

public class InputManager : MonoBehaviour
{
    private MyInput _myInput;

    public TabState tabState = TabState.None;

    public event Action<TabState> onAction_TabState;

    private void Awake()
    {
        _myInput = new MyInput(); 
        _myInput.Enable();
    }

    private void OnEnable()
    {
        _myInput.FirstInput.Tab.started += OnTabStarted;
        _myInput.FirstInput.Tab.performed += OnTabPerformed;
        _myInput.FirstInput.Tab.canceled += OnTabCanceled;
    }

    private void OnDisable()
    {
        _myInput.FirstInput.Tab.started -= OnTabStarted;
        _myInput.FirstInput.Tab.performed -= OnTabPerformed;
        _myInput.FirstInput.Tab.canceled -= OnTabCanceled;
    }
    //터치하는 세단계
    //started  <-> performed <-> canceled


    private void OnTabStarted(InputAction.CallbackContext context)
    {
      //  Debug.Log("Tab Start");
        tabState = TabState.Start;
        onAction_TabState?.Invoke(tabState);
    }

    private void OnTabPerformed(InputAction.CallbackContext context)
    {
      //  Debug.Log("Tab Performed");
        tabState = TabState.Hold;
        onAction_TabState?.Invoke(tabState);
    }


    private void OnTabCanceled(InputAction.CallbackContext context) 
    {
       // Debug.Log("Tab end");
        tabState = TabState.Canceld;
        onAction_TabState?.Invoke(tabState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
