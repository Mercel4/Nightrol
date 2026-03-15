using UnityEngine;

using Utility.Nightrol;

public class test : MonoBehaviour
{
    private GameData gameData;

    private void Start()
    {
        gameData = GameDataManager.Instance.Data;

        Debug.Log("Player Level: " + gameData.playerData.level);
        Debug.Log("Player Experience: " + gameData.playerData.experience);
        Debug.Log("Player Health: " + gameData.playerData.health);
        Debug.Log("Player Mana: " + gameData.playerData.mana);
        Debug.Log("Master Volume: " + gameData.settingsData.masterVolume);
    }
}
