using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System; // Coroutine kullanmak için ekledik

public class SupporterPool : MonoBehaviour
{
    public static SupporterPool Instance { get; private set; }

    public GameObject supporterPrefab;
    public Transform playerTransform;
    public float initialRadius = 1f;
    public float radius = 0.05f;
    public int poolSize = 100;
    private Queue<GameObject> supporterPool = new Queue<GameObject>();
    private List<GameObject> activeSupporters = new List<GameObject>();
    private List<Vector3> supporterOffsets = new List<Vector3>();

    public float rotationSpeed = 30f; // derece/saniye
    private float spreadMultiplier = 1f;
    public float maxSpread = 3f;
    public float maxXOfset = 3f;
    public float spreadSpeed = 1f;
    private bool isSpreading = false;
    public float spreadDuration = 5f; // Yayılma süresi (saniye)
    public Action onActiveSupporterCountChanged;

    public AudioSource pikachuSesiKaynagi; // Inspector'dan atayacağınız AudioSource

  
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
      

        for (int i = 0; i < poolSize; i++)
        {
            GameObject supporter = Instantiate(supporterPrefab);
            supporter.SetActive(false);
            supporterPool.Enqueue(supporter);
            SetSupporerParent(playerTransform.gameObject, supporter);
        }
    }
    private void Start()
    {
        onActiveSupporterCountChanged?.Invoke(); // 🔔 BURAYA EKLEDİK
    }
    public void SetSupporerParent(GameObject parent, GameObject child)
    {
        child.transform.SetParent(parent.transform);
        child.transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (isSpreading)
        {
            spreadMultiplier = Mathf.MoveTowards(spreadMultiplier, maxSpread, Time.deltaTime * spreadSpeed);
            // Yayılma tamamlandığında isSpreading'i false yapma işlemi Coroutine'da gerçekleşecek
        }
        else
        {
            spreadMultiplier = Mathf.MoveTowards(spreadMultiplier, 1f, Time.deltaTime * spreadSpeed * 2f); // Geri toplanma hızı
        }

     
        
        for (int i = 0; i < activeSupporters.Count; i++)
        {
            
            Vector3 rotated = supporterOffsets[i];
            Vector3 scaled = rotated * spreadMultiplier;
            activeSupporters[i].transform.position = playerTransform.position + scaled;
            supporterOffsets[i] = rotated; // Güncellenmiş rotasyonu sakla
        }
    }

    public void TriggerSpread()
    {
        isSpreading = true;
        StartCoroutine(ResetSpreadAfterDelay(spreadDuration));
    }

    private IEnumerator ResetSpreadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSpreading = false;
    }

    private void SetSuppoerterPos()
    {
        int activeCount = activeSupporters.Count;
        supporterOffsets.Clear();

        if (activeCount == 0 || playerTransform == null) return;

        float currentRadius = initialRadius * Mathf.Sqrt(activeCount);
        for (int i = 0; i < activeCount; i++)
        {
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * currentRadius;
            float offY= UnityEngine.Random.Range(-maxXOfset, 0);
            Vector3 offset = new Vector3((randomOffset.x>maxXOfset?randomOffset.x/2:randomOffset.x), 0f, -Mathf.Abs(randomOffset.y)+ offY);
            supporterOffsets.Add(offset);
            activeSupporters[i].transform.localPosition = offset; // Artık localPosition'ı ayarlıyoruz çünkü parent'a bağlılar
        }
    }

    public GameObject GetSupporter()
    {
        GameObject supporter = null;
        if (supporterPool.Count > 0)
        {
            supporter = supporterPool.Dequeue();
        }
        else
        {
            supporter = Instantiate(supporterPrefab);
        }

        supporter.SetActive(true);
        activeSupporters.Add(supporter);
        SetSupporerParent(playerTransform.gameObject, supporter);
        SetSuppoerterPos();
        onActiveSupporterCountChanged?.Invoke(); // 🔔 BURAYA EKLEDİK
        SoundManager.instance.PlayGameSound(SoundType.AddPika,0.05f); // Ses efektini burada çalıyoruz
        return supporter;
    }

    public void ReturnSupporter(int count)
    {
        if (activeSupporters.Count == 0 || count == 0) return;
        count = Mathf.Min(activeSupporters.Count, count);

        for (int i = 0; i < count; i++)
        {
            GameObject supporterToReturn = activeSupporters.First();
            activeSupporters.RemoveAt(0);
            supporterOffsets.RemoveAt(0);
            supporterToReturn.SetActive(false);
            supporterPool.Enqueue(supporterToReturn);

        }
        onActiveSupporterCountChanged?.Invoke(); // 🔔 BURAYA EKLEDİK
       // SoundManager.instance.PlayGameSound(SoundType.RedDor); // Ses efektini burada çalıyoruz
        SetSuppoerterPos();
    }

    public void ReturnSupporterGameobject(GameObject obj)
    {
        int index = activeSupporters.IndexOf(obj);
        if (index >= 0)
        {
            activeSupporters.RemoveAt(index);
            supporterOffsets.RemoveAt(index);
            obj.SetActive(false);
            supporterPool.Enqueue(obj);
            SetSuppoerterPos();
            onActiveSupporterCountChanged?.Invoke(); // 🔔 BURAYA EKLEDİK
            
        }
    }
    public int GetActiveCount() => activeSupporters.Count;

}