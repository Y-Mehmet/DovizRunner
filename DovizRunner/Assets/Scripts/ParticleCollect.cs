using UnityEngine;

public class ParticleCollect : MonoBehaviour
{
    public int destructionChance = 3;
    void OnParticleCollision(GameObject other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)  // E�er oyuncu kap�ya �arpt�ysa
        {

           
            icoll.DeCollect(1);

               
            
        }else
        {
            Debug.Log(other.name);
        }
       
        
    }
}
