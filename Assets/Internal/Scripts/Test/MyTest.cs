using UnityEngine;

public class MyTest : MonoBehaviour
{

    [SerializeField] private InputManager _inputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _inputManager.onAction_TabState += OnTabActionCallBack;   
    }

    private void OnDisable()
    {
        _inputManager.onAction_TabState -= OnTabActionCallBack;
    }

    public void OnTabActionCallBack(TabState state)
    {
        Debug.Log(state.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
