using UnityEngine;
using UnityEngine.InputSystem; // Yeni sistemin namespace'i

public class TouchInput : MonoBehaviour
{
    public static TouchInput Instance { get; private set; } // Singleton instance
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject); // E�er ba�ka bir instance varsa, bu nesneyi yok et
        }
    }
    public static event System.Action OnScreenTouched;
    public bool hasTouched = false;
    private bool isLoading = true; // Loading durumu

    private void Start()
    {
        // E�er loading ekran�nda isek, dokunmalar� engelle
        isLoading = true;
    }

    private void Update()
    {
        if (isLoading || hasTouched) return; // Loading s�ras�nda veya dokunulmu�sa i�lem yapma

        // Mobildeki ilk dokunu�u alg�la
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            hasTouched = true;
            SoundManager.instance.PlayGameSound(SoundType.Click); // Ses �al
            OnScreenTouched?.Invoke();
        }
    }

    // Loading bitti�inde dokunmay� kabul etmeye ba�la
    public void EndLoading()
    {
        isLoading = false;
    }
}
