using UnityEngine;

public class BaseCanvas : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Sahne ge�se de kaybolmas�n
    }
}
