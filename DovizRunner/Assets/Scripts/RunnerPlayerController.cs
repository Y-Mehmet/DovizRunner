using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerController : MonoBehaviour,ICollectible
{
    public float speed = 5f;  // �leri gitme h�z�
    public float minX = -4f;  // X ekseninin sol s�n�r�
    public float maxX = 4f;   // X ekseninin sa� s�n�r�
    public float moveSpeed = 2f; // Sa�/sol hareket h�z�

    private Vector3 targetPosition;
    private PlayerInput playerInput;
    private InputAction moveAction;
    public GameObject supporterPrefab;  // Destek�i objesi prefab'�
   // public Transform supporterSpawnPoint;  // Destek�ilerin spawn edilece�i nokta
    public float supporterDistance = -.2f;  // Destek�ilerin aralar�ndaki mesafe

    private int supporterCount = 0;  // �u anda oyuncunun sahip oldu�u destek�i say�s�

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); // PlayerInput bile�enini al
        moveAction = playerInput.actions["Move"]; // "Move" adl� input aksiyonunu al
        moveAction.performed += OnMove; // Olay� dinle
    }

    private void OnEnable()
    {
        moveAction.Enable(); // Input action'� etkinle�tir
    }

    private void OnDisable()
    {
        moveAction.Disable(); // Input action'� devre d��� b�rak
    }

    private void Update()
    {
        // S�rekli ileri git
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // Yumu�ak hareket ile x ekseninde hareket et
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // X eksenindeki hareketi al
        float moveInput = context.ReadValue<Vector2>().x;

        // X pozisyonunu g�ncelle
        transform.position += Vector3.right * moveInput * moveSpeed * Time.deltaTime;
    }
    public void SpawnSupporters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Singleton �zerinden havuzdan destek�iyi al
            GameObject supporter = SupporterPool.Instance.GetSupporter();

            // Destek�iyi oyuncunun etraf�na spawn et
            Vector3 spawnPosition = transform.position + new Vector3(i * supporterDistance, 0, 0);
            supporter.transform.position = spawnPosition;
        }

        supporterCount += count;
       // Debug.Log("Destek�i say�s�: " + supporterCount);
    }

    // Destek�i kaybetme fonksiyonu
    public void LoseSupporters(int count)
    {
        supporterCount = Mathf.Max(0, supporterCount - count);  // Destek�i say�s�n� kaybet, negatif olmas�n
        SupporterPool.Instance.ReturnSupporter( count);
       // Debug.Log("Kaybedilen destek�i say�s�: " + count);
    }

    
    public void OnCollect(int count = 1)
    {
        SpawnSupporters(count);  // Destek�ileri kazan
    }

    public void DeCollect(int count = 1)
    {
       LoseSupporters(count);  // Destek�ileri kaybet
        //Debug.Log("Destek�i kaybedildi: " + count);
    }
}
