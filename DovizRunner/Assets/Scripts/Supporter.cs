using UnityEngine;

public class Supporter : MonoBehaviour, ICollectible
{
    public float followSpeed = 2f;  // Takip hýzý
    public float followDistance = .1f;  // Oyuncuya olan mesafe
    private Transform playerTransform;  // Oyuncunun transformu
    private Vector3 randomOffset;  // Rastgele bir pozisyon kaymasý

    public void DeCollect(int count = 1)
    {
        if(SupporterPool.Instance != null   )  // Eðer havuz varsa
        {
            SupporterPool.Instance.ReturnSupporterGameobject(gameObject);  // Destekçiyi havuzdan çýkar
        }
        else
        {
            Debug.LogWarning("SupporterPool.Instance is null in Supporter.DeCollect");
        }
       // Destekçiyi havuza geri döndür
    }


        public void OnCollect(int count = 1)
    {
       
    }

 

    public void MultiplyCollect(int count = 1)
    {
        
    }
}
