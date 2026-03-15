using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    private static string saveFilePath =
        Application.persistentDataPath + "/gameData.json";

    // static init : only call once when start the game
    // 유니티의 Awake와 비슷하나, 유니티처럼 씬에 오브젝트가 있어야 할 필요 없음
    static SaveLoadSystem()
    {
        // Initialize any necessary data or settings here
        Debug.Log("SaveLoadSystem initialized. Save file path: " + saveFilePath);
    }

    public static GameData InitNewGame()
    {
        Debug.Log("Initializing new game data.");

        GameData freshData = new GameData(); // 기본값, 아주 깔끔한 새 게임 데이터 생성

        SaveGameData(freshData); // 새 게임 데이터 저장
        return freshData;
    }

    public static void SaveGameData(GameData data)
    {
        if (data == null)
        {
            Debug.LogError("Cannot save null game data.");
            return;
        }

        data.signature = ""; // 서명 초기화

        // HMAC 계산
        string jsonForHmac = JsonUtility.ToJson(data);
        data.checksum = HMACHelper.ComputeHMAC(jsonForHmac);

        // 최종 JSON 직렬화
        string json = JsonUtility.ToJson(data);
        byte[] bytesToUTF8 = UTF8Helper.StringToUTF8Bytes(json);

        // AES 암호화
        AESSecurityHelper.EncryptAndSave(bytesToUTF8, saveFilePath);
    }

    public static GameData LoadGameData()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found. Initializing new game data.");
            return InitNewGame();
        }
        
        // try-catch로 파일 읽기 및 복호화 과정 감싸기
        // 복보화 하는 민감한 과정은 실패할 경우 예기치 않은 결과를 초래할 수 있기 때문에, 예외 처리를 통해 안정성을 높이는 것이 좋음
        try
        {
            string json;

            // AES 복호화
            json = AESSecurityHelper.DecryptAndLoad(File.ReadAllBytes(saveFilePath), saveFilePath);

            // JSON 역직렬화
            GameData data = JsonUtility.FromJson<GameData>(json);

            if (data == null)
            {
                Debug.LogError("Failed to parse game data. Initializing new game data.");
                return InitNewGame();
            }

            // HMAC 검증
            string originalSignature = data.signature;
            data.signature = ""; // 서명 초기화

            // HMAC 계산
            string reCalculatedSignature = 
                HMACHelper.ComputeHMAC(JsonUtility.ToJson(data));
            
            if (originalSignature != reCalculatedSignature)
            {
                Debug.LogError("[SaveLoadSystem,HMAC] Data integrity check failed! Possible tampering detected. Initializing new game data.");

                // 데이터 변조(HMAC 검증 실패). 추가 로직 작성 필요
                return InitNewGame();
            }

            data.signature = originalSignature; // 서명 복원
            Debug.Log("Game data loaded successfully.");

            return data;
        }
        catch
        {
            return InitNewGame(); // 예외 발생 시 새 게임 데이터로 초기화
        }
    }
}