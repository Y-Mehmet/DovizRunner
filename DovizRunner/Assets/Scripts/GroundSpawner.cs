using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab1; // Ýlk renkli zemin prefabý
    public GameObject groundPrefab2; // Ýkinci renkli zemin prefabý
    public int numberOfTiles = 10; // Kaç tane zemin oluþturulacak
    public float tileLength = 5f; // Her bir zemin parçasýnýn uzunluðu

    private Vector3 nextSpawnPoint; // Yeni zeminin spawn noktasý
    private bool useFirstPrefab = true; // Hangi prefabý kullanacaðýmýzý belirler
    public  Transform envParent; // Zeminin toplanacaðý parent (Env)

    void Start()
    {
    

        // Baþlangýçta belirlenen sayýda zemin spawn et
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        // Hangi prefabý kullanacaðýmýzý belirle
        GameObject prefabToSpawn = useFirstPrefab ? groundPrefab1 : groundPrefab2;
        useFirstPrefab = !useFirstPrefab; // Her seferinde deðiþtir

        // Yeni zemini oluþtur
        GameObject newTile = Instantiate(prefabToSpawn, nextSpawnPoint, Quaternion.identity);
        newTile.transform.SetParent(envParent); // "Env" GameObject'inin altýna ekle

        // Yeni spawn noktasýný güncelle
        nextSpawnPoint += new Vector3(0, 0, tileLength);
    }
}
