using TMPro;
using UnityEngine;
using static Dervish;
using static Gate;

public class Dervish : MonoBehaviour
{
    [Tooltip("D�nme h�z� (derece/saniye cinsinden)")]
    public float rotationSpeed = 180f;

    [Tooltip("Hangi eksende d�necek?")]
    public Vector3 rotationAxis = Vector3.up;  // Y ekseni etraf�nda d�ner
    public enum DervishType { Real, Fake }
    public DervishType dervishType;  // Dervi� t�r�
    public int supporterCount = 5;  // Kap� ge�ti�inde ne kadar destek�i ekleyece�iz
    public TMP_Text supporterCountText;
    private void Start()
    {
        supporterCountText.text = supporterCount.ToString();
    }
    void Update()
    {
        // S�rekli kendi etraf�nda d�n
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)  // E�er oyuncu kap�ya �arpt�ysa
        {


            if (dervishType == DervishType.Real)
            {

                icoll.OnCollect(supporterCount);  // Destek�ileri ekle
            }
            else if (dervishType == DervishType.Fake)
            {
                // Negatif kap�: Kaybettirici etki
                icoll.DeCollect(supporterCount);  // Destek�ileri geri al
            }

            // Kap� ge�ildi, art�k kayboluyor
            gameObject.SetActive(false);
        }
    }
}

