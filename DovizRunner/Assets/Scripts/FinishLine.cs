using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICollectible>()!= null)
        {
           
           
            LoadingUI.Instance.ActivetedNextLevelPanel();

        }
    }
}
