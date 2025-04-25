using UnityEngine;

public class SceneMoveTest : MonoBehaviour
{
    public SceneName sceneName;
    public void OnButton_NextScene()
    {
       // SceneManagerController.Instance.SetTargetScene(sceneName);
        SceneManagerController.Instance.LoadScene(sceneName);
    }
}
