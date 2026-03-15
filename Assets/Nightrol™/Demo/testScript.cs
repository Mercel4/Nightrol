using UnityEngine;

using Utility.Nightrol;

public class testScript : MonoBehaviour
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameData.playerData.level += 1;
            gameData.playerData.experience += 100;
            gameData.playerData.health += 10;
            gameData.playerData.mana += 5;
            gameData.settingsData.masterVolume = Mathf.Clamp(gameData.settingsData.masterVolume + 0.1f, 0f, 1f);

            GameDataManager.Instance.Save();
            Debug.Log("Game data updated and saved.");
        }
    }
}
