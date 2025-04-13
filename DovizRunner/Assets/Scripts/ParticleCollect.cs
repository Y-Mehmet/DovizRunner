using UnityEngine;

public class ParticleCollect : MonoBehaviour
{
    public int destructionChance = 3;
    void OnParticleCollision(GameObject other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)  // E�er oyuncu kap�ya �arpt�ysa
        {

            Debug.Log("collcect fith particale "+other.name);
            icoll.DeCollect(1);

               
            
        }else
        {
            Debug.Log(other.name);
        }
       
        
    }
}
