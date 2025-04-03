using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab1; // �lk renkli zemin prefab�
    public GameObject groundPrefab2; // �kinci renkli zemin prefab�
    public int numberOfTiles = 10; // Ka� tane zemin spawn edilecek
    public float tileLength = 10f; // Her bir zemin par�as�n�n uzunlu�u

    private Vector3 nextSpawnPoint; // Yeni zeminin spawn noktas�
    private bool useFirstPrefab = true; // Hangi prefab�n spawn edilece�ini kontrol eder

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        // Hangi prefab� se�ece�imizi belirliyoruz (s�ras�yla de�i�ecek)
        GameObject prefabToSpawn = useFirstPrefab ? groundPrefab1 : groundPrefab2;
        useFirstPrefab = !useFirstPrefab; // Her seferinde de�i�tir

        // Yeni zemini olu�tur
        GameObject newTile = Instantiate(prefabToSpawn, nextSpawnPoint, Quaternion.identity);

        // Yeni spawn noktas�n� g�ncelle
        nextSpawnPoint += new Vector3(0, 0, tileLength);
    }
}
