using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum TabState
{
    None,
    Start,
    Hold,
    Canceld,
}

//일반적인 Raycast
// 내가 누르거나 탭한것이 UI인지 아닌지

public class InputManager : MonoBehaviour
{
    private MyInput _myInput;

    public RaycastHit rayHit { get; private set; }

    public Vector2 Pos { get; private set; }

    public bool IsUITouched { get; private set; } = false;

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

        _myInput.FirstInput.Pos.performed += OnPosStart;
    }

    private void OnDisable()
    {
        _myInput.FirstInput.Tab.started -= OnTabStarted;
        _myInput.FirstInput.Tab.performed -= OnTabPerformed;
        _myInput.FirstInput.Tab.canceled -= OnTabCanceled;

        _myInput.FirstInput.Pos.performed -= OnPosStart;
    }
    //터치하는 세단계
    //started  <-> performed <-> canceled

    #region Tab

    private void OnTabStarted(InputAction.CallbackContext context)
    {
        //  Debug.Log("Tab Start");
        IsUITouched = IsUITouch();
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
        rayHit = GetRayhit(Pos);
        if(rayHit.collider != null)
            Debug.Log(rayHit.collider.name);
        tabState = TabState.Canceld;
        onAction_TabState?.Invoke(tabState);
    }
    #endregion

    #region Pos

    private void OnPosStart(InputAction.CallbackContext context)
    {
        Pos = context.ReadValue<Vector2>();
       // Debug.Log(Pos);
    }

   

    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }


    private RaycastHit GetRayhit(Vector2 pos)
    {
        RaycastHit hit = default;

        if(Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("ray hit! : "+hit.collider.name);
                return hit;
            }
        }
        return hit;
    }


    private bool IsUITouch()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Pos
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerData, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var ray in raycastResults)
            {
                if (ray.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    Debug.Log("It's UI");
                    return true;
                }
            }
            return false;
        }
        return false;
    }
}
