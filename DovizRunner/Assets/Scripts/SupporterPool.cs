using UnityEngine;
using System.Collections.Generic;

public class SupporterPool : MonoBehaviour
{
    public static SupporterPool Instance { get; private set; }  // Singleton instance

    public GameObject supporterPrefab;  // Destekçi prefab'ý
    public int poolSize = 10;  // Havuzda bulundurulacak destekçi sayýsý
    private Queue<GameObject> supporterPool = new Queue<GameObject>();  // Destekçileri tutacak havuz
    private Queue<GameObject> activeSupporterPool = new Queue<GameObject>();  // Destekçileri tutacak havuz

    private void Awake()
    {
        // Eðer Instance daha önce atanmýþsa ve baþka bir kopya varsa, yok et.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; // Singleton instance'ý ayarla
        DontDestroyOnLoad(gameObject); // Sahne deðiþtiðinde yok olmasýný engelle

        // Havuzu oluþtur
        for (int i = 0; i < poolSize; i++)
        {
            GameObject supporter = Instantiate(supporterPrefab);
            supporter.SetActive(false);  // Baþlangýçta pasif hale getir
            supporterPool.Enqueue(supporter);  // Havuz listesine ekle
        }
    }

    // Havuzdan bir destekçi al
    public GameObject GetSupporter()
    {
        if (supporterPool.Count > 0)
        {
            GameObject supporter = supporterPool.Dequeue();  // Havuzdan bir destekçi al
            supporter.SetActive(true);  // Aktif hale getir
            activeSupporterPool.Enqueue(supporter);
            return supporter;
        }
        else
        {
            // Eðer havuzda destekçi kalmazsa, yeni bir tane oluþtur
            GameObject supporter = Instantiate(supporterPrefab);
            activeSupporterPool.Enqueue(supporter);
            return supporter;

        }
          // Aktif destekçileri tutacak havuza ekle
    }

    // Bir destekçiyi havuza geri koy
    public void ReturnSupporter(int count)
    {
        if(activeSupporterPool.Count == 0 ||  count ==0) return;  // Eðer aktif havuzda destekçi yoksa çýk
        count= activeSupporterPool.Count < count ? activeSupporterPool.Count : count; // Eðer havuzda yeterli destekçi yoksa, mevcut olaný kullan
        for (int i = 0; i < count; i++)
        {
            GameObject gameObject = activeSupporterPool.Dequeue();  // Aktif havuzdan bir destekçi al

            gameObject.SetActive(false);  // Pasif hale getir
            supporterPool.Enqueue(gameObject);  // Havuzun içine geri ekle
        }
     
    }
    public void ReturnSupporterGameobject(GameObject obj)
    {
        
           

            obj.SetActive(false);  // Pasif hale getir
            supporterPool.Enqueue(obj);  // Havuzun içine geri ekle
        Debug.Log("coll ile sportur silindi");
        
    }
}
