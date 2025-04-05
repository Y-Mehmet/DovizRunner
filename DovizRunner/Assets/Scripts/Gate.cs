using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum GateType { Positive, Negative }  // Pozitif ve negatif kap�lar
    public GateType gateType;  // Kap� t�r�
    public int supporterCount = 5;  // Kap� ge�ti�inde ne kadar destek�i ekleyece�iz
    public TMP_Text supporterCountText, dovizNameText;
    public string dovizName = "";

    private void Start()
    {
        dovizNameText.text = dovizName;
        // Kap� t�r�ne g�re destek�i say�s�n� ayarla
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
        if (icoll!= null)  // E�er oyuncu kap�ya �arpt�ysa
        {
          

            if (gateType == GateType.Positive)
            {

               icoll.OnCollect(supporterCount);  // Destek�ileri ekle
            }
            else if (gateType == GateType.Negative)
            {
                // Negatif kap�: Kaybettirici etki
                icoll.DeCollect(supporterCount);  // Destek�ileri geri al
            }

            // Kap� ge�ildi, art�k kayboluyor
            gameObject.SetActive(false);
        }
    }
}
