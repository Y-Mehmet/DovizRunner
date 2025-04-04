using UnityEngine;
using System.Collections.Generic;

public class SupporterPool : MonoBehaviour
{
    public static SupporterPool Instance { get; private set; }  // Singleton instance

    public GameObject supporterPrefab;  // Destek�i prefab'�
    public int poolSize = 10;  // Havuzda bulundurulacak destek�i say�s�
    private Queue<GameObject> supporterPool = new Queue<GameObject>();  // Destek�ileri tutacak havuz
    private Queue<GameObject> activeSupporterPool = new Queue<GameObject>();  // Destek�ileri tutacak havuz

    private void Awake()
    {
        // E�er Instance daha �nce atanm��sa ve ba�ka bir kopya varsa, yok et.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; // Singleton instance'� ayarla
        DontDestroyOnLoad(gameObject); // Sahne de�i�ti�inde yok olmas�n� engelle

        // Havuzu olu�tur
        for (int i = 0; i < poolSize; i++)
        {
            GameObject supporter = Instantiate(supporterPrefab);
            supporter.SetActive(false);  // Ba�lang��ta pasif hale getir
            supporterPool.Enqueue(supporter);  // Havuz listesine ekle
        }
    }

    // Havuzdan bir destek�i al
    public GameObject GetSupporter()
    {
        if (supporterPool.Count > 0)
        {
            GameObject supporter = supporterPool.Dequeue();  // Havuzdan bir destek�i al
            supporter.SetActive(true);  // Aktif hale getir
            activeSupporterPool.Enqueue(supporter);
            return supporter;
        }
        else
        {
            // E�er havuzda destek�i kalmazsa, yeni bir tane olu�tur
            GameObject supporter = Instantiate(supporterPrefab);
            activeSupporterPool.Enqueue(supporter);
            return supporter;

        }
          // Aktif destek�ileri tutacak havuza ekle
    }

    // Bir destek�iyi havuza geri koy
    public void ReturnSupporter(int count)
    {
        if(activeSupporterPool.Count == 0 ||  count ==0) return;  // E�er aktif havuzda destek�i yoksa ��k
        count= activeSupporterPool.Count < count ? activeSupporterPool.Count : count; // E�er havuzda yeterli destek�i yoksa, mevcut olan� kullan
        for (int i = 0; i < count; i++)
        {
            GameObject gameObject = activeSupporterPool.Dequeue();  // Aktif havuzdan bir destek�i al

            gameObject.SetActive(false);  // Pasif hale getir
            supporterPool.Enqueue(gameObject);  // Havuzun i�ine geri ekle
        }
     
    }
    public void ReturnSupporterGameobject(GameObject obj)
    {
        
           

            obj.SetActive(false);  // Pasif hale getir
            supporterPool.Enqueue(obj);  // Havuzun i�ine geri ekle
        Debug.Log("coll ile sportur silindi");
        
    }
}
