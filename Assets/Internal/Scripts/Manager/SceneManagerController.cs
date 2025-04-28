using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : SingleTon<SceneManagerController>
{
    private AsyncOperation _asyncOperation;
    public SceneName targetSceneName { get; private set; }

    public AsyncOperation AsyncOperation => _asyncOperation;
    //InitScene => GameScene가고싶어. GameScene이름을 담은채로 로딩씬으로가고 실제 로딩은
    // 로딩씬에서 하고 로딩씬 -> GameScene 가집니다.
    protected override void Awake()
    {
        base.Awake();
        
        Debug.Log("SceneManagerController Awake");
    }

  
    public AsyncOperation LoadSceneAsync()
    {
        Debug.Log($"target Scene is : {targetSceneName}");
        _asyncOperation = SceneManager.LoadSceneAsync(targetSceneName.ToString());
        return _asyncOperation;
    }

    public void LoadScene(SceneName sceneName)
    {
        targetSceneName = sceneName;
        SceneManager.LoadScene(SceneName.LoadingScene.ToString());
    }
  
}
