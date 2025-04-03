using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerController : MonoBehaviour
{
    public float speed = 5f; // Ýleri gitme hýzý
    public float laneDistance = 4f; // Þeritler arasý mesafe

    private int currentLane = 1; // 0: Sol, 1: Orta, 2: Sað
    private Vector3 targetPosition;
    private PlayerInput playerInput;
    private InputAction moveAction;

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
