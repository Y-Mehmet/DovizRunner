using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip coinSound;
    private GameController gameController;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        coinSound = Resources.Load<AudioClip>("coin");
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Obstacle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.name.Contains("Coin"))
        {
            Destroy(other.gameObject);
            if (audioSource && coinSound)
                audioSource.PlayOneShot(coinSound);

            if (gameController != null)
                gameController.AddScore(20f);
        }
    }
}
