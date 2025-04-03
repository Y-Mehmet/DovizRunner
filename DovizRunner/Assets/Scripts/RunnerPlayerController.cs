using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerController : MonoBehaviour
{
    public float speed = 5f; // �leri gitme h�z�
    public float laneDistance = 4f; // �eritler aras� mesafe

    private int currentLane = 1; // 0: Sol, 1: Orta, 2: Sa�
    private Vector3 targetPosition;
    private PlayerInput playerInput;
    private InputAction moveAction;

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
        targetPosition = new Vector3(currentLane * laneDistance - laneDistance, transform.position.y, transform.position.z + speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>(); // Vector2 olarak oku

        if (input.x > 0 && currentLane < 2)
        {
            currentLane++;
        }
        else if (input.x < 0 && currentLane > 0)
        {
            currentLane--;
        }
    }
}
