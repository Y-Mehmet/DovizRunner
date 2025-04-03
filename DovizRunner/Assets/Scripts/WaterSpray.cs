using UnityEngine;

public class WaterSpray : MonoBehaviour
{
 

   

    private void OnTriggerEnter(Collider other)
    {
        ICollectible icoll = other.GetComponent<ICollectible>();
        if( icoll!= null)
        {
            other.GetComponent<ICollectible>().DeCollect(0);
        }
        else
        {
            Debug.Log("icoll bulunamadý");
        }
        
    }
}
