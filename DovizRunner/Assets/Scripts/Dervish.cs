using UnityEngine;

public class Dervish : MonoBehaviour
{
    [Tooltip("D�nme h�z� (derece/saniye cinsinden)")]
    public float rotationSpeed = 180f;

    [Tooltip("Hangi eksende d�necek?")]
    public Vector3 rotationAxis = Vector3.up;  // Y ekseni etraf�nda d�ner

    void Update()
    {
        // S�rekli kendi etraf�nda d�n
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
