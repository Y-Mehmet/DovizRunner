using UnityEngine;

public class Supporter : MonoBehaviour, ICollectible
{
    public float followSpeed = 2f;  // Takip hýzý
    public float followDistance = .1f;  // Oyuncuya olan mesafe
    private Transform playerTransform;  // Oyuncunun transformu
    private Vector3 randomOffset;  // Rastgele bir pozisyon kaymasý

    public void DeCollect(int count = 1)
    {
        SupporterPool.Instance.ReturnSupporterGameobject(gameObject);  // Destekçiyi havuza geri döndür
    }


        public void OnCollect(int count = 1)
    {
       
    }

    void Start()
    {
        // Oyuncuyu bul
        GameObject player = GameObject.FindWithTag("Player");  // Oyuncuyu bulmak için "Player" tag'ini kullanýyoruz
        if (player != null)
        {
            playerTransform = player.transform;
            followSpeed = player.GetComponent<RunnerPlayerController>().speed + 2f; // Oyuncunun hareket hýzýný al
        }

        // Destekçiye rastgele bir offset ekle
        randomOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0f, Random.Range(-0.5f,- 0.2f));  // X ve Z ekseninde rastgele kaymalar
    }

    void Update()
    {
        // Eðer oyuncu varsa, ona doðru hareket et
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position + randomOffset;  // Rastgele offset'i oyuncuya ekle
            targetPosition.y = transform.position.y; // Y ekseninde yer deðiþtirmesin, sadece X ve Z'yi takip etsin

            // Destekçiyi oyuncunun etrafýnda belirli bir mesafeye göre hareket ettir
            Vector3 direction = targetPosition - transform.position;
            float distance = direction.magnitude;

            // Eðer mesafe belirlenen uzaklýktan fazla ise, oyuncuyu takip et
            if (distance > followDistance)
            {
                transform.position += direction.normalized * followSpeed * Time.deltaTime;
            }
        }
    }
}
