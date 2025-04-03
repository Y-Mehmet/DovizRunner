using UnityEngine;

public class Supporter : MonoBehaviour, ICollectible
{
    public float followSpeed = 2f;  // Takip h�z�
    public float followDistance = .1f;  // Oyuncuya olan mesafe
    private Transform playerTransform;  // Oyuncunun transformu
    private Vector3 randomOffset;  // Rastgele bir pozisyon kaymas�

    public void DeCollect(int count = 1)
    {
        SupporterPool.Instance.ReturnSupporterGameobject(gameObject);  // Destek�iyi havuza geri d�nd�r
    }


        public void OnCollect(int count = 1)
    {
       
    }

    void Start()
    {
        // Oyuncuyu bul
        GameObject player = GameObject.FindWithTag("Player");  // Oyuncuyu bulmak i�in "Player" tag'ini kullan�yoruz
        if (player != null)
        {
            playerTransform = player.transform;
            followSpeed = player.GetComponent<RunnerPlayerController>().speed + 2f; // Oyuncunun hareket h�z�n� al
        }

        // Destek�iye rastgele bir offset ekle
        randomOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0f, Random.Range(-0.5f,- 0.2f));  // X ve Z ekseninde rastgele kaymalar
    }

    void Update()
    {
        // E�er oyuncu varsa, ona do�ru hareket et
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position + randomOffset;  // Rastgele offset'i oyuncuya ekle
            targetPosition.y = transform.position.y; // Y ekseninde yer de�i�tirmesin, sadece X ve Z'yi takip etsin

            // Destek�iyi oyuncunun etraf�nda belirli bir mesafeye g�re hareket ettir
            Vector3 direction = targetPosition - transform.position;
            float distance = direction.magnitude;

            // E�er mesafe belirlenen uzakl�ktan fazla ise, oyuncuyu takip et
            if (distance > followDistance)
            {
                transform.position += direction.normalized * followSpeed * Time.deltaTime;
            }
        }
    }
}
