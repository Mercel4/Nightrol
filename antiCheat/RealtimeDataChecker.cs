using UnityEngine;

public class RealtimeDataChecker : MonoBehaviour
{
    private GameData currentData;
    private string lastChecksum;
    private float checkInterval = 0.05f;
    private float timer = 0f;

    void Start()
    {
        currentData = SaveLoadSystem.LoadGameData();
        lastChecksum = SaveLoadSystem.ComputeChecksum(JsonUtility.ToJson(currentData));
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckDataIntegrity();
        }
    }

    void CheckDataIntegrity()
    {
        // 1. Memory tampering check
        string memChecksum = SaveLoadSystem.ComputeChecksum(JsonUtility.ToJson(currentData));
        if (memChecksum != lastChecksum)
        {
            Debug.LogWarning("Memory tampering detected!");
            infoUI.isUseCheat = true;

            // reset to default values
            currentData = new GameData();
            SaveLoadSystem.SaveGameData(currentData);
            lastChecksum = SaveLoadSystem.ComputeChecksum(JsonUtility.ToJson(currentData));
            return;
        }

        // 2. File tampering check (detected inside LoadGameData)
        GameData loadedData = SaveLoadSystem.LoadGameData();

        string fileChecksum = SaveLoadSystem.ComputeChecksum(JsonUtility.ToJson(loadedData));
        if (fileChecksum != lastChecksum)
        {
            Debug.LogWarning("File tampering detected!");
            infoUI.isUseCheat = true;

            // reset to default values
            currentData = new GameData();
            SaveLoadSystem.SaveGameData(currentData);
            lastChecksum = SaveLoadSystem.ComputeChecksum(JsonUtility.ToJson(currentData));
        }
    }

    // [Modified] Call this method to update the baseline checksum when legitimate data changes
    public void UpdateChecksum(GameData data)
    {
        currentData = data;
        lastChecksum = SaveLoadSystem.ComputeChecksum(JsonUtility.ToJson(currentData));
    }
    
}