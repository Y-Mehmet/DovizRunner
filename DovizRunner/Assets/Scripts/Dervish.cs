using UnityEngine;

public class Dervish : MonoBehaviour
{
    [Tooltip("Dönme hýzý (derece/saniye cinsinden)")]
    public float rotationSpeed = 180f;

    [Tooltip("Hangi eksende dönecek?")]
    public Vector3 rotationAxis = Vector3.up;  // Y ekseni etrafýnda döner

    void Update()
    {
        // Sürekli kendi etrafýnda dön
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
