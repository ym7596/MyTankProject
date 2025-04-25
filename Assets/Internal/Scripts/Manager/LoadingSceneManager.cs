using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(LoadSceneTargetCo());
       
    }

    private IEnumerator LoadSceneTargetCo()
    {
        yield return new WaitForSeconds(1.5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(
            SceneManagerController.Instance.targetSceneName.ToString()
            );

        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress}");
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        asyncLoad.allowSceneActivation = true;
    }
}
