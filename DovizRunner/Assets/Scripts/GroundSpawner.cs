using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab1; // �lk renkli zemin prefab�
    public GameObject groundPrefab2; // �kinci renkli zemin prefab�
    public int numberOfTiles = 10; // Ka� tane zemin olu�turulacak
    public float tileLength = 5f; // Her bir zemin par�as�n�n uzunlu�u

    private Vector3 nextSpawnPoint; // Yeni zeminin spawn noktas�
    private bool useFirstPrefab = true; // Hangi prefab� kullanaca��m�z� belirler
    public  Transform envParent; // Zeminin toplanaca�� parent (Env)

    void Start()
    {
    

        // Ba�lang��ta belirlenen say�da zemin spawn et
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        // Hangi prefab� kullanaca��m�z� belirle
        GameObject prefabToSpawn = useFirstPrefab ? groundPrefab1 : groundPrefab2;
        useFirstPrefab = !useFirstPrefab; // Her seferinde de�i�tir

        // Yeni zemini olu�tur
        GameObject newTile = Instantiate(prefabToSpawn, nextSpawnPoint, Quaternion.identity);
        newTile.transform.SetParent(envParent); // "Env" GameObject'inin alt�na ekle

        // Yeni spawn noktas�n� g�ncelle
        nextSpawnPoint += new Vector3(0, 0, tileLength);
    }
}
