using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

// PlayerData
[System.Serializable]
public class PlayerData
{
    
}

// SettingsData
[System.Serializable]
public class SettingsData
{
    
}

// Unified Data Class
[System.Serializable]
public class GameData
{
    // When creating a data class at the top, add it below in the following format
    public PlayerData playerData = new PlayerData();
    public SettingsData settingsData = new SettingsData();

    // checksum field to store the value before JSON encryption. Do not modify this field directly.
    public string checksum = ""; 
}