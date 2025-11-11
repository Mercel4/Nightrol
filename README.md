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
4. [How To Use in Unity](#how-to-use-in-unity)

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

# How To Use in Unity
이제 Unity로 돌아갑시다.
스크립트 폴더를 만들어서 antiCheat와 Data를 그대로 넣어 컴파일 해줍니다. 스크립트 폴더는 선택으로 만드시면 됩니다.

Hierarchy에서 새로운 빈 오브젝트를 만듭니다. 이 오브젝트는 무결성 모듈의 매니저가 됩니다.

<img width="248" height="55" alt="Image" src="https://github.com/user-attachments/assets/b247f0ee-a4bd-45e2-b372-1e3ef1c1d463" />

자식 오브젝트를 추가합니다. 하나는 매번 무결성모듈을 실행하여 검사하는것, 나머지 하나는 변조가 감지되면 즉시 변조씬으로 이동하는 로직이 담긴 오브젝트 입니다.
**해당 사진은 예시이며, 오브젝트의 이름은 언제나 변경하여도 무관합니다.**

사진 기준, infoUI오브젝트에는 infoUI.cs를 컴포넌트에 추가합니다.

사진 기준, realTimeCheakAntiCheatManager오브젝트에는 RealtimeDataChecker.cs를 컴포넌트에 추가합니다.

이러면, 80%는 도달했습니다. 이제 변조가 감지되었을때 해당 씬으로 넘어가게 하면 끝입니다.
Scene/Nightrol.unity를 Project에 넣습니다.
하지만 불러올 경우, Hierarchy에서 Canvas/Image가 Missing이 되어 흰색화면이 노출 될 것입니다.

**이는 정상적인 작동이며, 이미지를 추가하면 됩니다. 이미지를 추가할때 유의 사항은 맨 아래 라이선스 탭에서 확인하십시오.**

<img width="3840" height="2160" alt="Image" src="https://github.com/user-attachments/assets/cf53c786-ce56-4ca3-b589-ff90f4981175" />
이미지 예시. 데이터는 초기화 되는 로직은 리포지토리에 있으나, esc키를 누루면 자동으로 게임이 종료되는 로직은 직접 입력해야 합니다.

**놀랍지만, 이게 전부입니다. 모든 로직이 정상작동 할 것입니다.**
