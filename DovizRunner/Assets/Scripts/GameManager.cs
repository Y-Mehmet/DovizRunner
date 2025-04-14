using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isGameStart= false;
    public static int levelCount = 1;  // Sahne say�s�
private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (SaveManager.HasSave())
        {
            var data = SaveManager.Load();
           levelCount = data.levelCount;
        }
    }
    public void �ncraseLevelCount()
    {
        levelCount++;
    }
    public void LoadNextLevel()
    {
        �ncraseLevelCount();
        TouchInput.Instance.hasTouched = false; // Dokunma ge�ersiz
        LoadingUI.Instance.tapToStartPanel.SetActive(true); // Tap to start paneli aktif
        SceneCycleManager.Instance.LoadNextScene();
    }
    


}
