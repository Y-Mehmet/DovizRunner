using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerController : MonoBehaviour, ICollectible
{
    public float speed = 5f;         // Ýleri gitme hýzý
    public float minX = -4f;         // X ekseninin sol sýnýrý
    public float maxX = 4f;          // X ekseninin sað sýnýrý
    public float moveSpeed = 2f;     // Sað/sol hareket hýzý

    private Vector3 targetPosition;
    private PlayerInput playerInput;
    private InputAction moveAction;

    private float moveInputX = 0f;   // Input sisteminden alýnan X hareket deðeri
    private Rigidbody rb;            // Rigidbody referansý

    public GameObject supporterPrefab;
    public float supporterDistance = -.2f;
    private int supporterCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        moveAction.performed += OnMove;
        moveAction.canceled += OnStopMove;
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInputX = context.ReadValue<Vector2>().x;
    }

    private void OnStopMove(InputAction.CallbackContext context)
    {
        moveInputX = 0f;
    }

    private void FixedUpdate()
    {
        // X ve Y yönünde hareket belirle
        float newX = moveInputX * moveSpeed;
        float newY = speed;

        // Rigidbody'ye velocity uygula
        rb.linearVelocity = new Vector3(newX, 0, speed);

        // X sýnýrlarýný uygula
        float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
        rb.position = new Vector3(clampedX, rb.position.y, rb.position.z);
    }




    public void SpawnSupporters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject supporter = SupporterPool.Instance.GetSupporter();
            Vector3 spawnPosition = transform.position + new Vector3(i * supporterDistance, 0, 0);
            supporter.transform.position = spawnPosition;
        }

        supporterCount += count;
    }

    public void LoseSupporters(int count)
    {
        supporterCount = Mathf.Max(0, supporterCount - count);
        SupporterPool.Instance.ReturnSupporter(count);
    }

    public void OnCollect(int count = 1)
    {
        SpawnSupporters(count);
    }

    public void DeCollect(int count = 1)
    {
        LoseSupporters(count);
    }
}
