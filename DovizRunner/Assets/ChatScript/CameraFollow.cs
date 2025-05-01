using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Oyuncu transformu
    public Vector3 offset;    // Kamera ile oyuncu arasýndaki mesafe
    public float smoothSpeed = 0.125f;  // Takip etme hýzý
    void Start()
    {
        if (player == null)  // Eðer player manuel olarak atanmadýysa
        {
            StartCoroutine(StartWaiter());  // Coroutine baþlat
        }
    }
    IEnumerator StartWaiter()
    {
        // Baþlangýçta kamerayý oyuncunun konumuna ayarlýyoruz
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                
            }
            else
            {
                break;
            }
        }
        yield return null;
    }


    void LateUpdate()
    {
        if (player != null)
        {
            // Kamera konumunu oyuncunun konumuna göre ayarlýyoruz
            Vector3 desiredPosition = player.position + new Vector3(0,5,-15);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Kameranýn oyuncuya bakmasýný saðlýyoruz
            transform.LookAt(player);
        }
    }
}
