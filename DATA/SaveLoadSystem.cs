using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public static class SaveLoadSystem
{
    // path is safely initialized in the static constructor
    private static string path;
    private static readonly string key = "0000000000000000"; // This is a 16-byte AES key. Replace this key with your own immediately before use.
    private static readonly string iv  = "0000000000000000"; // This is a 16-byte AES IV. Replace this IV with your own immediately before use.

    // static constructor: executed when the class is first used
    static SaveLoadSystem()
    {
        path = Application.persistentDataPath + "/data.json"; // Input your desired file name here
        Debug.Log("SaveLoadSystem initialized. Data path: " + path);
    }

    // Save GameData
    public static void SaveGameData(GameData data)
    {
        // Initialize before checksum calculation
        data.checksum = "";
        string jsonForChecksum = JsonUtility.ToJson(data);
        data.checksum = ComputeChecksum(jsonForChecksum);

        // JSON Serialization
        string json = JsonUtility.ToJson(data);

        // Save files after AES encryption
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            File.WriteAllBytes(path, encrypted);
        }
    }

    // Load GameData
    public static GameData LoadGameData()
    {
        if (!File.Exists(path)) return new GameData(); // if no file, return new GameData

        try
        {
            byte[] encrypted = File.ReadAllBytes(path);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);

                string json = Encoding.UTF8.GetString(decrypted);
                GameData data = JsonUtility.FromJson<GameData>(json);

                // Checksum verification
                string originalChecksum = data.checksum;
                data.checksum = ""; 
                string recalculated = ComputeChecksum(JsonUtility.ToJson(data));

                if (originalChecksum != recalculated)
                {
                    Debug.LogWarning("Data tampering detected! (Checksum mismatch)");
                    infoUI.isUseCheat = true;

                    // reset to default values
                    GameData resetData = new GameData();
                    SaveGameData(resetData);
                    return resetData;
                }

                data.checksum = originalChecksum;
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Data tampering detected! (Decryption failed)\n" + e.Message);
            infoUI.isUseCheat = true;

            // reset to default values
            GameData resetData = new GameData();
            SaveGameData(resetData);
            return resetData;
        }
    }

    // Checksum calculation
    public static string ComputeChecksum(string json)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            byte[] hash = sha.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
