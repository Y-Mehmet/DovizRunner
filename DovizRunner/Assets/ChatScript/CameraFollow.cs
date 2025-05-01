using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Oyuncu transformu
    public Vector3 offset;    // Kamera ile oyuncu aras�ndaki mesafe
    public float smoothSpeed = 0.125f;  // Takip etme h�z�
    void Start()
    {
        if (player == null)  // E�er player manuel olarak atanmad�ysa
        {
            StartCoroutine(StartWaiter());  // Coroutine ba�lat
        }
    }
    IEnumerator StartWaiter()
    {
        // Ba�lang��ta kameray� oyuncunun konumuna ayarl�yoruz
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
            // Kamera konumunu oyuncunun konumuna g�re ayarl�yoruz
            Vector3 desiredPosition = player.position + new Vector3(0,5,-15);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Kameran�n oyuncuya bakmas�n� sa�l�yoruz
            transform.LookAt(player);
        }
    }
}
