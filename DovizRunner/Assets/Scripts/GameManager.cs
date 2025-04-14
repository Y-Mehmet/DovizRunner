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
            DontDestroyOnLoad(gameObject); // Sahne ge�se de kaybolmas�n
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void �ncraseLevelCount()
    {
        levelCount++;
    }
    
    

}
