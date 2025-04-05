using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum GateType { Positive, Negative }  // Pozitif ve negatif kapýlar
    public GateType gateType;  // Kapý türü
    public int supporterCount = 5;  // Kapý geçtiðinde ne kadar destekçi ekleyeceðiz
    public TMP_Text supporterCountText, dovizNameText;
    public string dovizName = "";

    private void Start()
    {
        dovizNameText.text = dovizName;
        // Kapý türüne göre destekçi sayýsýný ayarla
        if (gateType == GateType.Positive)
        {
            supporterCountText.text = "+" + supporterCount.ToString();
        }
        else if (gateType == GateType.Negative)
        {
            supporterCountText.text = "-" + supporterCount.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll!= null)  // Eðer oyuncu kapýya çarptýysa
        {
          

            if (gateType == GateType.Positive)
            {

               icoll.OnCollect(supporterCount);  // Destekçileri ekle
            }
            else if (gateType == GateType.Negative)
            {
                // Negatif kapý: Kaybettirici etki
                icoll.DeCollect(supporterCount);  // Destekçileri geri al
            }

            // Kapý geçildi, artýk kayboluyor
            gameObject.SetActive(false);
        }
    }
}
