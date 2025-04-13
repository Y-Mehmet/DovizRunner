using UnityEngine;

public class Barikat : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ICollectible>() != null)
        {
            Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
            pushDirection.y = 0;
            pushDirection.z = 0; // sadece X yönünde it

            collision.gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * 500f); // kuvvet uygula
        }
    }
}
