using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isGameStart= false;
    public static int levelCount = 1;  // Sahne sayýsý
private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçse de kaybolmasýn
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ÝncraseLevelCount()
    {
        levelCount++;
    }
    
    

}
