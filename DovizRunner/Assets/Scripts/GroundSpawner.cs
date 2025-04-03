using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab1; // Ýlk renkli zemin prefabý
    public GameObject groundPrefab2; // Ýkinci renkli zemin prefabý
    public int numberOfTiles = 10; // Kaç tane zemin spawn edilecek
    public float tileLength = 10f; // Her bir zemin parçasýnýn uzunluðu

    private Vector3 nextSpawnPoint; // Yeni zeminin spawn noktasý
    private bool useFirstPrefab = true; // Hangi prefabýn spawn edileceðini kontrol eder

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        // Hangi prefabý seçeceðimizi belirliyoruz (sýrasýyla deðiþecek)
        GameObject prefabToSpawn = useFirstPrefab ? groundPrefab1 : groundPrefab2;
        useFirstPrefab = !useFirstPrefab; // Her seferinde deðiþtir

        // Yeni zemini oluþtur
        GameObject newTile = Instantiate(prefabToSpawn, nextSpawnPoint, Quaternion.identity);

        // Yeni spawn noktasýný güncelle
        nextSpawnPoint += new Vector3(0, 0, tileLength);
    }
}
