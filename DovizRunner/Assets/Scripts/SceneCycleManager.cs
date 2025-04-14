using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCycleManager : MonoBehaviour
{
    public static SceneCycleManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçse de kaybolmasýn
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;

        int nextSceneIndex = (currentSceneIndex + 1) % totalSceneCount; // Döngü: sona geldiyse baþa dön
       


        SceneManager.LoadScene(nextSceneIndex);
    }
}
