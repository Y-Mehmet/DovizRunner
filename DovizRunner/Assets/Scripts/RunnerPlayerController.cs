using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerController : MonoBehaviour, ICollectible
{
    public float speed = 5f;
    public float minX = -4f;
    public float maxX = 4f;
    public float xMoveSpeed = 0.7f;

    private Vector3 targetPosition;
    private PlayerInput playerInput;
    private InputAction moveAction;

    private float moveInputX = 0f;
    private Rigidbody rb;

    public GameObject supporterPrefab;
    public float supporterDistance = -0.2f;
    private int supporterCount = 0;
    Animator animator;

    private bool isRunning = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        moveAction.performed += OnMove;
        moveAction.canceled += OnStopMove;

        isRunning = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        TouchInput.OnScreenTouched += StartRunning;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        TouchInput.OnScreenTouched -= StartRunning;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", isRunning);
    }
    private void StartRunning()
    {
        isRunning = true;
        animator.SetBool("isRunning", isRunning);
        //Debug.Log("Game Started");
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
        if (!isRunning)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        float newX = moveInputX * xMoveSpeed;
        float newY = speed;

        rb.linearVelocity = new Vector3(newX, 0, speed);

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

