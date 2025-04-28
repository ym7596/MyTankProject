using UnityEngine;
using UnityEngine.UI;

public class UILoadingScene : MonoBehaviour
{
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private LoadingSceneManager _loadingSceneManager;

    private void OnEnable()
    {
       _loadingSceneManager.onAction_progress += SliderPregress;
    }

    private void OnDisable()
    {
        _loadingSceneManager.onAction_progress -= SliderPregress;
    }

    private void SliderPregress(float value)
    {
        Debug.Log(value);
        _loadingSlider.value = value;
    }
}
