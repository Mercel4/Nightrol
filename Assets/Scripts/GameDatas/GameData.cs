using System;

[Serializable]
public class PlayerData
{
    public int level = 1;
    public int experience = 0;
    public int health = 100;
    public int mana = 50;
    // Add more player-related fields as needed
}

[Serializable]
public class SettingsData
{
    public float masterVolume = 0.5f;
}

[Serializable]
public class GameData
{
    public PlayerData playerData = new PlayerData();
    public SettingsData settingsData = new SettingsData();

    public string checksum = "";
    public string signature = "";
}