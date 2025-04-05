using UnityEngine;

public class ParticleCollect : MonoBehaviour
{
    public int destructionChance = 3;
    void OnParticleCollision(GameObject other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)  // E�er oyuncu kap�ya �arpt�ysa
        {
            int randomValue = Random.Range(0, 100);
            if (true)
            {
                icoll.DeCollect(1);
              //  Debug.LogWarning("ParticleCollect:  destruction");
            }
            else
            {
                Debug.Log("ParticleCollect: No destruction");
            }
        }
       
        
    }
}
