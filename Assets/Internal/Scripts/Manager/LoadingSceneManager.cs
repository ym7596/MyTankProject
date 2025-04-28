using System;
using System.Collections;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{

    public event Action<float> onAction_progress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(LoadSceneTargetCo());
       
    }

    private IEnumerator LoadSceneTargetCo()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManagerController.Instance.LoadSceneAsync();

        AsyncOperation asyncLoad = SceneManagerController.Instance.AsyncOperation;

        asyncLoad.allowSceneActivation = false;

        float fakeProgress = 0f;

        while (asyncLoad.isDone == false)
        {
            float targetProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            fakeProgress = Mathf.MoveTowards(fakeProgress, targetProgress, Time.deltaTime);

            //Debug.Log($"Loading progress: {asyncLoad.progress}");
            onAction_progress?.Invoke(asyncLoad.progress);

            if(asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        
    }
}
