using UnityEngine;

public class Barikat : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ICollectible>() != null)
        {
           
        }
    }
}
