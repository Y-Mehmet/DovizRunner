using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum GateType { Positive, Negative }  // Pozitif ve negatif kap�lar
    public GateType gateType;  // Kap� t�r�
    public int supporterCount = 5;  // Kap� ge�ti�inde ne kadar destek�i ekleyece�iz

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
