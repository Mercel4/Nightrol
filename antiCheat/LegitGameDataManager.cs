using UnityEngine;

public static class LegitGameDataManager
{
    public static void ApplyChange(GameData data, System.Action<GameData> changeAction)
    // GameData is GameData. System.Action<GameData> means a function without a return value.
    {
        // 1. Validation
        if (data == null || changeAction == null)
            return;

        // 2. Apply changes
        changeAction(data);

        // 3. Save
        SaveLoadSystem.SaveGameData(data);

        // 4. Update checksum
        var checker = GameObject.FindObjectOfType<RealtimeDataChecker>();
        if (checker != null)
        {
            checker.UpdateChecksum(data);
        }
    }
}