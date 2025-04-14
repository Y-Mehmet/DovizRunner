using UnityEngine;
using System.Collections;

public class WaterSpray : MonoBehaviour
{
    
    public int killCount = 5;

    private void OnTriggerEnter(Collider other)
    {
    

        ICollectible icoll = other.GetComponent<ICollectible>();
        if (icoll != null)
        {
            icoll.DeCollect(killCount);
            SoundManager.instance.PlayGameSound(SoundType.Water);
        }
        else
        {
            Debug.Log("icoll bulunamadý");
        }
    }

  
}
