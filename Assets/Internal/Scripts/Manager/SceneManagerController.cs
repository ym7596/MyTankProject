using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : SingleTon<SceneManagerController>
{
    public SceneName targetSceneName { get; private set; }
    //InitScene => GameScene가고싶어. GameScene이름을 담은채로 로딩씬으로가고 실제 로딩은
    // 로딩씬에서 하고 로딩씬 -> GameScene 가집니다.
    protected override void Awake()
    {
        base.Awake();
        
        Debug.Log("SceneManagerController Awake");
    }

  

    public void LoadScene(SceneName sceneName)
    {
        targetSceneName = sceneName;
        SceneManager.LoadScene(SceneName.LoadingScene.ToString());
    }

    public void LoadSceneAsync(SceneName sceneName)
    {
        targetSceneName = sceneName;
        SceneManager.LoadSceneAsync(SceneName.LoadingScene.ToString());
    }
}
