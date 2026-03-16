using System.IO;
using UnityEngine;
using Utility.Nightrol;

namespace Utility.Nightrol
{
    public static class SaveLoadSystem
    {
        private static string saveFilePath = Application.persistentDataPath + "/gameData.json";

        static SaveLoadSystem()
        {
            Debug.Log("[Nightrol™] SaveLoadSystem initialized. Path: " + saveFilePath);
        }

        public static GameData InitNewGame()
        {
            Debug.Log("[Nightrol™] Initializing new game data.");
            GameData freshData = new GameData();
            SaveGameData(freshData);
            return freshData;
        }

        public static void SaveGameData(GameData data)
        {
            if (data == null)
            {
                Debug.LogError("[Nightrol™] Cannot save null game data.");
                return;
            }

            var config = GameDataManager.Instance.SecurityConfig;
            if (config == null)
            {
                Debug.LogError("[Nightrol™] SecurityConfig is missing! Cannot save.");
                return;
            }

            // Step 1: Prepare data for HMAC
            data.signature = ""; 
            string jsonForHmac = JsonUtility.ToJson(data);

            // Step 2: Generate HMAC signature
            data.signature = HMACHelper.ComputeHMAC(jsonForHmac, config.hmacKey);

            // Step 3: Finalize JSON and Encrypt
            string finalJson = JsonUtility.ToJson(data);
            byte[] bytesToUTF8 = UTF8Helper.StringToUTF8Bytes(finalJson);

            AESSecurityHelper.EncryptAndSave(bytesToUTF8, saveFilePath, config.aesKey, config.aesIV);
        }

        public static GameData LoadGameData()
        {
            if (!File.Exists(saveFilePath))
            {
                Debug.LogWarning("[Nightrol™] Save file not found. Creating new data.");
                return InitNewGame();
            }

            var config = GameDataManager.Instance.SecurityConfig;
            if (config == null)
            {
                Debug.LogError("[Nightrol™] SecurityConfig is missing! Loading aborted.");
                return InitNewGame();
            }

            string json = "";

            // Step 1: AES Decryption
            try
            {
                byte[] fileBytes = File.ReadAllBytes(saveFilePath);
                json = AESSecurityHelper.DecryptAndLoad(fileBytes, saveFilePath, config.aesKey, config.aesIV);
            }
            catch (System.Exception e)
            {
                // This captures file corruption or unauthorized text modification
                Debug.LogError($"[Nightrol™ Security - Step 1] AES Decryption Failed: {e.Message}");
                return InitNewGame();
            }

            // Step 2: JSON Parsing
            GameData data = JsonUtility.FromJson<GameData>(json);
            if (data == null)
            {
                Debug.LogError("[Nightrol™ Security - Step 2] JSON Parsing Failed: Data is null or corrupted.");
                return InitNewGame();
            }

            // Step 3: HMAC Integrity Check
            string originalSignature = data.signature;
            data.signature = ""; // Clear for recalculation

            string currentJson = JsonUtility.ToJson(data);
            string reCalculatedSignature = HMACHelper.ComputeHMAC(currentJson, config.hmacKey);

            if (originalSignature != reCalculatedSignature)
            {
                // This captures subtle value tampering (e.g., changing level 1 to 99)
                Debug.LogError("[Nightrol™ Security - Step 3] HMAC Verification Failed: Data tampering detected!");
                return InitNewGame();
            }

            // Success
            data.signature = originalSignature; 
            Debug.Log("<color=green>[Nightrol™] Security check passed. Game data loaded successfully.</color>");
            return data;
        }
    }
}