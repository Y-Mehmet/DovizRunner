using TMPro;
using UnityEngine;
using static Dervish;
using static Gate;

public class Dervish : MonoBehaviour
{
    [Tooltip("Dönme hýzý (derece/saniye cinsinden)")]
    public float rotationSpeed = 180f;

    [Tooltip("Hangi eksende dönecek?")]
    public Vector3 rotationAxis = Vector3.up;  // Y ekseni etrafýnda döner
    public enum DervishType { Real, Fake }
    public DervishType dervishType;  // Derviþ türü
    public int supporterCount = 5;  // Kapý geçtiðinde ne kadar destekçi ekleyeceðiz
    public TMP_Text supporterCountText;
    private void Start()
    {
        supporterCountText.text = supporterCount.ToString();
    }
    void Update()
    {
        // Sürekli kendi etrafýnda dön
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)  // Eðer oyuncu kapýya çarptýysa
        {


            if (dervishType == DervishType.Real)
            {

                icoll.OnCollect(supporterCount);  // Destekçileri ekle
            }
            else if (dervishType == DervishType.Fake)
            {
                // Negatif kapý: Kaybettirici etki
                icoll.DeCollect(supporterCount);  // Destekçileri geri al
            }

            // Kapý geçildi, artýk kayboluyor
            gameObject.SetActive(false);
        }
    }
}

