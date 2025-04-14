using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI Instance
    { get; private set; }
    public Slider loadingBar;
    public GameObject loadingPanel;
    public GameObject tapToStartPanel;
    public GameObject loadNextLevelPanel;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(SimulateLoading());
    }
    private void OnEnable()
    {
        TouchInput.OnScreenTouched += DisActiveStartPanel;
    }
    private void OnDisable()
    {
        TouchInput.OnScreenTouched -= DisActiveStartPanel;
    }
    private IEnumerator SimulateLoading()
    {
        loadingPanel.SetActive(true);
        tapToStartPanel.SetActive(false);

        float loadProgress = 0f;
        while (loadProgress < 1f)
        {
            loadProgress += Time.deltaTime * 0.5f;
            loadingBar.value = loadProgress;
            yield return null;
        }

        loadingPanel.SetActive(false);
        tapToStartPanel.SetActive(true);
        // Loading ekran� bitti�i yerden sonra, bu metod �a�r�l�r
        TouchInput.Instance.EndLoading();  // Dokunmalar art�k ge�erli

    }
    public void DisActiveStartPanel()
    {
        tapToStartPanel.SetActive(false);
    }
    public void ActivetedNextLevelPanel()
    {
        loadNextLevelPanel.SetActive(true);
    }
}
