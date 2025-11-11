<p align='center'>
    <img src="https://capsule-render.vercel.app/api?type=waving&color=auto&height=300&section=header&text=Nightrol%20Integrity%20Module&fontSize=70&animation=fadeIn&fontAlignY=38&desc=POTENTIAL%20PROJECT%E2%84%A2%20independently%20developed&descAlignY=51&descAlign=62"/>
</p>

<p align='center'>
  <a href="https://www.instagram.com/be_sl1t8/">
    <img src="https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=Instagram&logoColor=white&link=https://www.instagram.com/be_sl1t8/">
  </a>
  <a href="#demo">
    <img src="https://img.shields.io/badge/DOWNLOAD%20EULA%20-%234FC08D.svg?&style=for-the-badge&&logoColor=white"/>
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/DONATE%20-%235c86fa.svg?&style=for-the-badge&&logoColor=white"/>
  </a>
</p> 

## Navigation

1. [Introduce](#introduce)
2. [Requirements](#requirements)
3. [How To Use](#how-to-use)

# Introduce

The **POTENTIAL PROJECT™ Integrity Module** is a proprietary system developed to ensure the security, fairness, and stability of the POTENTIAL PROJECT™ game environment. This module actively monitors and detects unauthorized modifications, tampering attempts, and any irregularities that could compromise gameplay integrity. By integrating this module, developers can safeguard against cheating, protect user data, and maintain a consistent experience across all game instances.

Designed with flexibility in mind, the module can be seamlessly integrated into the project with minimal effort — simply adding the provided script is sufficient for full functionality. The system is fundamentally based on **JSON**, and incorporates a robust **game data storage system**, **AES-based encryption**, and **checksum verification** to detect any data tampering. It continuously monitors data integrity at **50ms intervals**, while allowing legitimate data changes through exception handling mechanisms. Upon detecting unauthorized modifications, the module can trigger a scene transition, and developers can further customize the response logic as needed. Key features include real-time data alteration detection, critical file verification, and automated integrity violation responses, all rigorously tested to minimize performance impact while maintaining security and reliability.

# Requirements
- Unity 2020.3 LTS or Over
- Works only with C# scripts
- Supports Windows/Mac platforms

# How To Use
사용하기 전 DATA/SaveLoadSystem에서 Key와 IV key를 16바이트로 즉시 수정하십시오.
SaveLoadSystem에서 "/data.json"에 이름을 변경할 수 있습니다. 단 확장자는 변경하지 마십시오.

<img width="1072" height="314" alt="Image" src="https://github.com/user-attachments/assets/a787a266-7c01-412f-abbe-5e000e436afa" />

DATA/gameData.cs에서 자신의 등록할 데이터를 지정하십시오.
일반적인 JSON저장 방식과 비슷합니다.
public string checksum = ""; 는 절대로 수정 및 삭제 하지 마십시오.

<img width="733" height="284" alt="Image" src="https://github.com/user-attachments/assets/b43f67e5-86bb-4b6c-92e3-49ec6e3dfd2d" />

데이터를 gameData.PlayerData.PlayerNick = "Mercel04"와 같이 변경을 하면 합법적인 변경임에 불구하고 무결성 모듈에 걸리게 됩니다. 이를 해결하기 위해서 우리는 하나의 예외 처리를 해야합니다. 다행이 antiCheat/LegitGameDataManager.cs에 예외 처리를 위한 클래스가 마련되여 있습니다. 다음 사진과 같은 양식으로 적으시면 됩니다.

<img width="409" height="196" alt="Image" src="https://github.com/user-attachments/assets/352661d8-3581-4e11-a6fc-a5a57fd10f8d" />

LegitGameDataManager.ApplyChange(gameData, (data) => {}); 형식이며 중괄호 { } 안에 데이터를 변경하는 코드를 넣으면 됩니다. 예를들어. gameData.PlayerData.PlayerNick = "Mercel04"; 이렇게요.

**중요 : 무조건 게임 데이터를 조작할 경우 gameData = SaveLoadSystem.LoadGameData();를 Awake()또는 Start()에 넣어야 합니다.**
