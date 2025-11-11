using UnityEngine;

public static class LegitGameDataManager
{
    public static void ApplyChange(GameData data, System.Action<GameData> changeAction)
    {
        // 1. Validation
        if (data == null || changeAction == null)
            return;

        // 2. Apply changes
        changeAction(data);

        // 3. Save
        SaveLoadSystem.SaveGameData(data);

        // 4. Update checksum
#if UNITY_2023_1_OR_NEWER
        var checker = Object.FindFirstObjectByType<RealtimeDataChecker>();
#else
        var checker = Object.FindObjectOfType<RealtimeDataChecker>();
#endif

        if (checker != null)
        {
            checker.UpdateChecksum(data);
        }
    }
}
