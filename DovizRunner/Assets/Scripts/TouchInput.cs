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
            Destroy(gameObject); // Eðer baþka bir instance varsa, bu nesneyi yok et
        }
    }
    public static event System.Action OnScreenTouched;
    public bool hasTouched = false;
    private bool isLoading = true; // Loading durumu

    private void Start()
    {
        // Eðer loading ekranýnda isek, dokunmalarý engelle
        isLoading = true;
    }

    private void Update()
    {
        if (isLoading || hasTouched) return; // Loading sýrasýnda veya dokunulmuþsa iþlem yapma

        // Mobildeki ilk dokunuþu algýla
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            hasTouched = true;
            SoundManager.instance.PlayGameSound(SoundType.Click); // Ses çal
            OnScreenTouched?.Invoke();
        }
    }

    // Loading bittiðinde dokunmayý kabul etmeye baþla
    public void EndLoading()
    {
        isLoading = false;
    }
}
