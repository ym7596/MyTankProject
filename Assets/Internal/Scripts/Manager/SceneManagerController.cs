using UnityEngine;

public class SceneManagerController : SingleTon<SceneManagerController>
{

    protected override void Awake()
    {
        base.Awake();
        
        Debug.Log("SceneManagerController Awake");
    }
}
