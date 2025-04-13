using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text supporterCount;
    
    private void Start()
    {
        if (SupporterPool.Instance != null)
        {
            SupporterPool.Instance.onActiveSupporterCountChanged += HandleSupporterCountChanged;
            HandleSupporterCountChanged(); // Ba�lang��ta say�y� g�stermek i�in
        }
        else
        {
            Debug.LogWarning("SupporterPool.Instance is null in UIManager.Start");
        }
    }

    private void OnDisable()
    {
        if (SupporterPool.Instance != null)
            SupporterPool.Instance.onActiveSupporterCountChanged -= HandleSupporterCountChanged;
    }

    private void HandleSupporterCountChanged()
    {
        int currentCount = SupporterPool.Instance.GetActiveCount();
        supporterCount.text= ""+currentCount;
        //Debug.Log("Supporter Count: " + currentCount);
    }

   
}
