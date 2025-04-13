using UnityEngine;

public class Supporter : MonoBehaviour, ICollectible
{
    public float followSpeed = 2f;  // Takip h�z�
    public float followDistance = .1f;  // Oyuncuya olan mesafe
    private Transform playerTransform;  // Oyuncunun transformu
    private Vector3 randomOffset;  // Rastgele bir pozisyon kaymas�

    public void DeCollect(int count = 1)
    {
        if(SupporterPool.Instance != null   )  // E�er havuz varsa
        {
            SupporterPool.Instance.ReturnSupporterGameobject(gameObject);  // Destek�iyi havuzdan ��kar
        }
        else
        {
            Debug.LogWarning("SupporterPool.Instance is null in Supporter.DeCollect");
        }
       // Destek�iyi havuza geri d�nd�r
    }


        public void OnCollect(int count = 1)
    {
       
    }

    void Start()
    {
        
    }

    void LateUpdate()
    {
        //if (playerTransform != null)
        //{
        //    Vector3 targetPosition = playerTransform.position + randomOffset;
        //    targetPosition.y = transform.position.y;

        //    float distance = Vector3.Distance(transform.position, targetPosition);
        //    if (distance > followDistance)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        //    }
        //}
    }
}
