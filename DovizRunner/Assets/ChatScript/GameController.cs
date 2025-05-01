using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject groundPrefab;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public GameObject canvasGO;  // Canvas UI
    private Text scoreText;
    private Text gameOverText;
    private Button restartButton;

    private Transform playerTransform;
    private float spawnZ = 0f;
    private float tileLength = 10f;
    private int tilesOnScreen = 7;
    private List<GameObject> activeTiles = new List<GameObject>();

    private Vector3[] lanes = new Vector3[] {
        new Vector3(-3f, 0f, 0f),
        new Vector3(0f, 0f, 0f),
        new Vector3(3f, 0f, 0f)
    };

    private Vector3 currentLane;
    private int currentLaneIndex = 1;
    private float score = 0f;
    private AudioSource audioSource;
    private AudioClip coinSound;

    void Start()
    {
        Application.targetFrameRate = 60;
        CreatePrefabs();
        SpawnPlayer();
        currentLane = lanes[currentLaneIndex];
        CreateScoreUI();
        CreateGameOverUI();

        for (int i = 0; i < tilesOnScreen; i++)
            SpawnTile();
    }
    void CreatePrefabs()
    {
        // Coin
        Material yellowMat = Resources.Load<Material>("Materials/YellowMat");
      
        
        
        coinPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        coinPrefab.transform.localScale = Vector3.one * 0.5f;
        coinPrefab.GetComponent<Renderer>().material = yellowMat;
        coinPrefab.GetComponent<Renderer>().material.color = Color.yellow;
        coinPrefab.name = "Coin";
        coinPrefab.AddComponent<SphereCollider>().isTrigger = true;
        coinPrefab.SetActive(false);

        // Player
        playerPrefab = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        playerPrefab.name = "Player";
        playerPrefab.transform.localScale = new Vector3(1, 2, 1);
        Rigidbody rb = playerPrefab.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        playerPrefab.AddComponent<PlayerCollision>();

        // Zemin
        groundPrefab = GameObject.CreatePrimitive(PrimitiveType.Plane);
        groundPrefab.name = "GroundTile";
        groundPrefab.transform.localScale = new Vector3(1, 1, 1);

        // Engel
        obstaclePrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstaclePrefab.name = "Obstacle";
        obstaclePrefab.transform.localScale = new Vector3(1, 1, 1);
        Rigidbody orb = obstaclePrefab.AddComponent<Rigidbody>();
        orb.isKinematic = true;
        obstaclePrefab.tag = "Obstacle";
    }

    void Update()
    {
        if (Time.timeScale == 0) return;  // Game Over'da işlem yapma

        MovePlayerForward();

        if (playerTransform.position.z - tileLength > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteOldestTile();
        }

        DetectSwipe();
        UpdateScore();
    }

    #region UI
    void CreateScoreUI()
    {
        canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Skor yazısı
        GameObject scoreTextGO = new GameObject("ScoreText");
        scoreTextGO.transform.SetParent(canvasGO.transform);
        scoreText = scoreTextGO.AddComponent<Text>();

        scoreText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        scoreText.fontSize = 48;
        scoreText.color = Color.white;
        scoreText.alignment = TextAnchor.UpperCenter;
        scoreText.rectTransform.anchoredPosition = new Vector2(0, -100);
        scoreText.rectTransform.sizeDelta = new Vector2(600, 200);
        scoreText.rectTransform.anchorMin = new Vector2(0.5f, 1f);
        scoreText.rectTransform.anchorMax = new Vector2(0.5f, 1f);
    }

    void CreateGameOverUI()
    {
        // Game Over yazısı
        GameObject gameOverTextGO = new GameObject("GameOverText");
        gameOverTextGO.transform.SetParent(canvasGO.transform);
        gameOverText = gameOverTextGO.AddComponent<Text>();

        gameOverText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        gameOverText.fontSize = 64;
        gameOverText.color = Color.red;
        gameOverText.alignment = TextAnchor.MiddleCenter;
        gameOverText.rectTransform.anchoredPosition = new Vector2(0, 100);
        gameOverText.rectTransform.sizeDelta = new Vector2(600, 200);
        gameOverText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        gameOverText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        gameOverText.text = "";

        // Yeniden Başlat Butonu
        GameObject restartButtonGO = new GameObject("RestartButton");
        restartButtonGO.transform.SetParent(canvasGO.transform);
        restartButton = restartButtonGO.AddComponent<Button>();
        restartButtonGO.AddComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
        restartButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -200);

        Text buttonText = new GameObject("ButtonText").AddComponent<Text>();
        buttonText.transform.SetParent(restartButtonGO.transform);
        buttonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        buttonText.fontSize = 24;
        buttonText.color = Color.white;
        buttonText.alignment = TextAnchor.MiddleCenter;
        buttonText.text = "Restart Game";

        restartButton.onClick.AddListener(RestartGame);
        restartButton.gameObject.SetActive(false);  // Başlangıçta gizle
    }

    void ShowGameOverUI()
    {
        gameOverText.text = "Game Over!";
        restartButton.gameObject.SetActive(true);  // Yeniden başlatma butonunu göster
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Aynı sahneyi yeniden yükle
    }
    #endregion

    #region Score & Player
    void UpdateScore()
    {
        score += Time.deltaTime * 10f;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        player.name = "Player";
        player.tag = "Player";
        playerTransform = player.transform;
        player.AddComponent<Rigidbody>();
        Material redMat = Resources.Load<Material>("Materials/RedMat");

        // Player'a renk eklemek için Renderer kullanıyoruz
        Renderer playerRenderer = player.GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerRenderer.material = redMat;
        }
    }


    void MovePlayerForward()
    {
        // Rigidbody üzerinden hareket etmeyi sağlıyoruz
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 7f); // Y eksenindeki hız korunuyor, Z ekseninde hız artıyor
        }
    }


    #endregion
    void SwipeRight()
    {
        if (currentLaneIndex < lanes.Length - 1)
        {
            currentLaneIndex++;
            UpdateLane();
        }
    }

    void SwipeLeft()
    {
        if (currentLaneIndex > 0)
        {
            currentLaneIndex--;
            UpdateLane();
        }
    }

    void UpdateLane()
    {
        currentLane = lanes[currentLaneIndex];
        Vector3 newPos = new Vector3(currentLane.x, playerTransform.position.y, playerTransform.position.z);
        playerTransform.position = newPos;  // Yeni pozisyonu set ediyoruz
    }

    #region Tile & Obstacles
    void SpawnTile()
    {
        GameObject tile = Instantiate(groundPrefab, Vector3.forward * spawnZ, Quaternion.identity);
        activeTiles.Add(tile);
        SpawnObstacle(tile.transform.position);
        spawnZ += tileLength;
    }

    void DeleteOldestTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    void SpawnObstacle(Vector3 tilePos)
    {
        int randomLane = Random.Range(0, lanes.Length);
        Vector3 spawnPos = tilePos + lanes[randomLane] + new Vector3(0, 0.5f, Random.Range(2f, 8f));
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
    #endregion

    #region Swipe Detection
    Vector2 startTouch, endTouch;

    void DetectSwipe()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            startTouch = touch.position;
        else if (touch.phase == TouchPhase.Ended)
        {
            endTouch = touch.position;
            Vector2 delta = endTouch - startTouch;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > 50)
            {
                if (delta.x > 0)
                    SwipeRight();
                else
                    SwipeLeft();
            }
        }
    }

    
    public void AddScore(float amount)
    {
        score += amount;
    }

    
    #endregion

    #region Collision Detection
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0f;  // Oyun bitti
            ShowGameOverUI();  // Game Over ekranını göster
        }
    }
    #endregion
}
