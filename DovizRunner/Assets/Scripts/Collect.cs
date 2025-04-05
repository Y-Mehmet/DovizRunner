using UnityEngine;
using static Dervish;

public class Collect : MonoBehaviour
{
    public int supporterCount=0;
    private void OnTriggerEnter(Collider other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)  // E�er oyuncu kap�ya �arpt�ysa
        {


            if (supporterCount>0)
            {

                icoll.OnCollect(supporterCount);  // Destek�ileri ekle
            }
            else
            {
                // Negatif kap�: Kaybettirici etki
                icoll.DeCollect(-supporterCount);  // Destek�ileri geri al
            }

            // Kap� ge�ildi, art�k kayboluyor
            gameObject.SetActive(false);
        }
    }
}
