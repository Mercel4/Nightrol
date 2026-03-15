using System;
using System.Security.Cryptography;
using System.Text;

public static class HMACHelper
{
    public static string ComputeHMAC(string json)
    {
        using  // using은 해당 코드가 끝나면 자동으로 메모리를 제거하는 역할을 함
        (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecurityKeys.hmacKey)))
        {
            byte[] hashBytes = 
                hmac.ComputeHash(Encoding.UTF8.GetBytes(json));
            
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}