using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class AESSecurityHelper
{

    public static void EncryptAndSave(byte[] dataToEncrypt, string filePath)
    {
        using (Aes aes = Aes.Create())
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(SecurityKeys.aesKey);
            byte[] ivBytes = Encoding.UTF8.GetBytes(SecurityKeys.aesIV);
            
            if (keyBytes.Length != 16 && keyBytes.Length != 24 && keyBytes.Length != 32)
            {
                Debug.LogError($"[AES] 키 길이가 {keyBytes.Length}바이트입니다. 반드시 16, 24, 32자 중 하나여야 합니다!");
                return;
            }

            if (ivBytes.Length != 16)
            {
                Debug.LogError($"[AES] IV 길이가 {ivBytes.Length}바이트입니다. 반드시 16자여야 합니다!");
                return;
            }

            aes.Key = Encoding.UTF8.GetBytes(SecurityKeys.aesKey);
            aes.IV = Encoding.UTF8.GetBytes(SecurityKeys.aesIV);

            byte[] encrypted = aes.CreateEncryptor()
                .TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);

            File.WriteAllBytes(filePath, encrypted);
            Debug.Log($"[AES] Data saved successfully to: {filePath}");
        }
    }

    public static string DecryptAndLoad(byte[] encryptedData, string filePath)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(SecurityKeys.aesKey);
            aes.IV = Encoding.UTF8.GetBytes(SecurityKeys.aesIV);

            byte[] decrypted = aes.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            Debug.Log($"[AES] Data loaded and decrypted successfully from: {filePath}");

            return UTF8Helper.UTF8BytesToString(decrypted);
        }
    }
}