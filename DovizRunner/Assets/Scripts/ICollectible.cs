using UnityEngine;

public interface ICollectible 
{
   public void OnCollect(int count=1);
    public void DeCollect(int count = 1);


}
