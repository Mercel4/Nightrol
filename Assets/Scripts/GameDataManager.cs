using UnityEngine;

namespace GamePlay.Utility.Nightrol
{
    // 해당 코드는 반드시 Hierarchy에 GameDataManager라는 이름으로 존재해야 함
    // 또한 Hierarchy의 최상단위일 필요는 없으나, 자식으로 넣으려면 부모도 같이 살아남게 설정해야 하므로 복잡해질 수 있음
    // 따라서 최상위에 독립적으로 두는 것이 가장 안전하고 일반적인 방식

    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance { get; private set; }
        public bool isReady => Data != null;
        public GameData Data { get; private set; }

        private void Awake()
        {
            if (Instance != null) // 인스턴스는 하나만 존재해야 하므로 있다면 자신을 파괴
            {
                Destroy(gameObject);
                return;
            }

            Instance = this; // 자신을 인스턴스로 설정
            DontDestroyOnLoad(gameObject);

            Data = SaveLoadSystem.LoadGameData();
        }

        public void Save()
        {
            SaveLoadSystem.SaveGameData(Data);
        }
    }
}