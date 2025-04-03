using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerController : MonoBehaviour,ICollectible
{
    public float speed = 5f;  // Ýleri gitme hýzý
    public float minX = -4f;  // X ekseninin sol sýnýrý
    public float maxX = 4f;   // X ekseninin sað sýnýrý
    public float moveSpeed = 2f; // Sað/sol hareket hýzý

    private Vector3 targetPosition;
    private PlayerInput playerInput;
    private InputAction moveAction;
    public GameObject supporterPrefab;  // Destekçi objesi prefab'ý
   // public Transform supporterSpawnPoint;  // Destekçilerin spawn edileceði nokta
    public float supporterDistance = -.2f;  // Destekçilerin aralarýndaki mesafe

    private int supporterCount = 0;  // Þu anda oyuncunun sahip olduðu destekçi sayýsý

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); // PlayerInput bileþenini al
        moveAction = playerInput.actions["Move"]; // "Move" adlý input aksiyonunu al
        moveAction.performed += OnMove; // Olayý dinle
    }

    private void OnEnable()
    {
        moveAction.Enable(); // Input action'ý etkinleþtir
    }

    private void OnDisable()
    {
        moveAction.Disable(); // Input action'ý devre dýþý býrak
    }

    private void Update()
    {
        // Sürekli ileri git
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // Yumuþak hareket ile x ekseninde hareket et
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // X eksenindeki hareketi al
        float moveInput = context.ReadValue<Vector2>().x;

        // X pozisyonunu güncelle
        transform.position += Vector3.right * moveInput * moveSpeed * Time.deltaTime;
    }
    public void SpawnSupporters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Singleton üzerinden havuzdan destekçiyi al
            GameObject supporter = SupporterPool.Instance.GetSupporter();

            // Destekçiyi oyuncunun etrafýna spawn et
            Vector3 spawnPosition = transform.position + new Vector3(i * supporterDistance, 0, 0);
            supporter.transform.position = spawnPosition;
        }

        supporterCount += count;
       // Debug.Log("Destekçi sayýsý: " + supporterCount);
    }

    // Destekçi kaybetme fonksiyonu
    public void LoseSupporters(int count)
    {
        supporterCount = Mathf.Max(0, supporterCount - count);  // Destekçi sayýsýný kaybet, negatif olmasýn
        SupporterPool.Instance.ReturnSupporter( count);
       // Debug.Log("Kaybedilen destekçi sayýsý: " + count);
    }

    
    public void OnCollect(int count = 1)
    {
        SpawnSupporters(count);  // Destekçileri kazan
    }

    public void DeCollect(int count = 1)
    {
       LoseSupporters(count);  // Destekçileri kaybet
        //Debug.Log("Destekçi kaybedildi: " + count);
    }
}
