using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    [System.Serializable]
    public class SaveData
    {
        public int levelCount;
    }

    public static void Save()
    {
        SaveData data = new SaveData
        {
            levelCount = GameManager.levelCount
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);

        Debug.Log("Game saved to: " + savePath);
    }

    public static SaveData Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found.");
            return null;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            Save();
    }
    public static bool HasSave() => File.Exists(savePath);
}
