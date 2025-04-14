using UnityEngine;

public class BaseCanvas : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Sahne geçse de kaybolmasýn
    }
}
