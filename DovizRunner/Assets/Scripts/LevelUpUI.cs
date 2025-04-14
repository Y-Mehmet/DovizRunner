using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public TMP_Text levelCountText;
    public Button ContinuNextLevelBTn;
   
    private void OnEnable()
    {
        levelCountText.text =GameManager.levelCount.ToString();
        ContinuNextLevelBTn.onClick.AddListener(ContinueNextLevel);
    }
    private void OnDisable()
    {
        ContinuNextLevelBTn.onClick.RemoveListener(ContinueNextLevel);
    }
    void ContinueNextLevel()
    {
        SoundManager.instance.PlayGameSound(SoundType.Click,1); // Ses çal
        GameManager.instance.LoadNextLevel();
        StartCoroutine(ClosePanel());
        
      
        
    }
    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(.92f);
        gameObject.SetActive(false);
    }
}
