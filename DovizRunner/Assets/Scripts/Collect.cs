using UnityEngine;
using static Dervish;

public class Collect : MonoBehaviour
{
    public int supporterCount=0;
    private void OnTriggerEnter(Collider other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)  // Eðer oyuncu kapýya çarptýysa
        {


            if (supporterCount>0)
            {

                icoll.OnCollect(supporterCount);  // Destekçileri ekle
            }
            else
            {
                // Negatif kapý: Kaybettirici etki
                icoll.DeCollect(-supporterCount);  // Destekçileri geri al
            }

            // Kapý geçildi, artýk kayboluyor
            gameObject.SetActive(false);
        }
    }
}
