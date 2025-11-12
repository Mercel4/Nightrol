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
5. [Nightrol™ Integrity Module End-User License Agreement (EULA)](#nightrol™-integrity-module-end-user-license-agreement(EULA))


# Introduce

The **POTENTIAL PROJECT™ Integrity Module** is a proprietary system developed to ensure the security, fairness, and stability of the POTENTIAL PROJECT™ game environment. This module actively monitors and detects unauthorized modifications, tampering attempts, and any irregularities that could compromise gameplay integrity. By integrating this module, developers can safeguard against cheating, protect user data, and maintain a consistent experience across all game instances.

Designed with flexibility in mind, the module can be seamlessly integrated into the project with minimal effort — simply adding the provided script is sufficient for full functionality. The system is fundamentally based on **JSON**, and incorporates a robust **game data storage system**, **AES-based encryption**, and **checksum verification** to detect any data tampering. It continuously monitors data integrity at **50ms intervals**, while allowing legitimate data changes through exception handling mechanisms. Upon detecting unauthorized modifications, the module can trigger a scene transition, and developers can further customize the response logic as needed. Key features include real-time data alteration detection, critical file verification, and automated integrity violation responses, all rigorously tested to minimize performance impact while maintaining security and reliability.

# Requirements
- Unity 2020.3 LTS or Over
- Works only with C# scripts
- Supports Windows, Mac, iOS, Android platforms

# How To Use
Before use, immediately modify the Key and IV keys to 16 bytes in DATA/SaveLoadSystem.
You can rename the file to "/data.json" in SaveLoadSystem, but do not change the extension.

<img width="1072" height="314" alt="Image" src="https://github.com/user-attachments/assets/a787a266-7c01-412f-abbe-5e000e436afa" />

Specify your own registered data in DATA/gameData.cs.
It's similar to the standard JSON storage method.
Never modify or delete public string checksum = "";

<img width="733" height="284" alt="Image" src="https://github.com/user-attachments/assets/b43f67e5-86bb-4b6c-92e3-49ec6e3dfd2d" />

Changing data like gameData.PlayerData.PlayerNick = "Mercel04" will cause the integrity module to fail, even though it's a legitimate change. To resolve this, we need to handle an exception. Fortunately, antiCheat/LegitGameDataManager.cs provides a class for exception handling. Simply write it in the format shown below.

<img width="409" height="196" alt="Image" src="https://github.com/user-attachments/assets/352661d8-3581-4e11-a6fc-a5a57fd10f8d" />

The format is LegitGameDataManager.ApplyChange(gameData, (data) => {}); and you can put the code that changes the data inside the curly brackets { }. For example, gameData.PlayerData.PlayerNick = "Mercel04";

**Important: If you ever manipulate game data, you must put gameData = SaveLoadSystem.LoadGameData(); in Awake() or Start().**

# How To Use in Unity
Now, let's go back to Unity.
Create a script folder and place AntiCheat and Data there to compile. You can create the script folder by selecting it.

Create a new empty object in the Hierarchy. This object will be the manager of the Integrity module.

<img width="248" height="55" alt="Image" src="https://github.com/user-attachments/assets/b247f0ee-a4bd-45e2-b372-1e3ef1c1d463" />

Add child objects. One object will run the integrity module every time to check for tampering, and the other will contain logic that will immediately jump to the tampering scene if tampering is detected. **This image is an example; you can change the object names at any time.**

Based on the photo, add infoUI.cs to the component in the infoUI object.

This should get you 80% of the way there. Now, all you need to do is jump to the relevant scene when tampering is detected.
Place Scene/Nightrol.unity into your project.
However, when you load it, the Canvas/Image will be missing from the Hierarchy, resulting in a white screen.

**This is normal operation, and you can simply add the image. Please check the License tab at the bottom for any additional information when adding images.**

<img width="3840" height="2160" alt="Image" src="https://github.com/user-attachments/assets/cf53c786-ce56-4ca3-b589-ff90f4981175" />
Image example. The data initialization logic is in the repository, but the logic for automatically ending the game when the ESC key is pressed must be manually implemented.

**Surprisingly, that's all there is to it. All logic will work as expected.**

# Nightrol™ Integrity Module End-User License Agreement (EULA)

You can download the EULA as a .pdf by clicking the Download button at the top.

**Please read before use.**

**Version 1.2 – Last Updated: November 2025**  
**Copyright © POTENTIAL PROJECT™**

---

## Article 1 (Purpose)
This End-User License Agreement (hereinafter “Agreement”) is a legal contract between **POTENTIAL PROJECT™** (hereinafter “Licensor”) and any individual or entity using this software (hereinafter “User”).  
It defines the terms under which the **Nightrol™ Integrity Module** (hereinafter “Software”) may be used.  

By downloading, installing, or using the Software, the User is deemed to have agreed to all terms of this Agreement.  
If you do not agree, do not install or use the Software.

---

## Article 2 (Grant of License)
1. Licensor grants the User a **non-exclusive**, **non-transferable**, **revocable**, royalty-free license to use the Software.  
2. The User may use the Software in both commercial and non-commercial projects.  
3. The following conditions must be observed:  
   - The project or game must clearly credit **“POTENTIAL PROJECT™”** in at least one of the following: credits, introduction screen, or documentation.  
   - The Software may not be redistributed or sold independently.  
   - Including the Software in a commercial project is allowed, provided proper attribution is given (e.g., “Integrity Module by POTENTIAL PROJECT™”).  
   - Ownership or trademark rights of the Software are **not transferred** to the User.

---

## Article 3 (Restrictions)
The User may not:  
1. Remove or modify copyright or trademark notices.  
2. Change the name of the infoUI object from **“Nightrol™”**.  
   - Any renaming requires prior **written permission** from POTENTIAL PROJECT™.  
3. Replace the infoUI logo; it must use the **Nightrol™ logo**.  
   - Logo changes require prior **written permission**.  
4. Use the Software for illegal, malicious, or unethical purposes (e.g., data tampering, unauthorized access, cheating).  
5. Sell, lease, redistribute, or relicence the Software as a standalone product.

---

## Article 4 (Ownership)
All rights, title, and intellectual property rights in the Software remain with **POTENTIAL PROJECT™**.  
The User only obtains a license to use the Software under this Agreement.  
The Software and its materials are protected by international copyright and intellectual property laws.

---

## Article 5 (Attribution)
All projects using this Software must credit **POTENTIAL PROJECT™** in at least one of the following ways:  
- Game credits (e.g., “Integrity Module by POTENTIAL PROJECT™”)  
- In-game information screen (About / Info UI)  
- Official website or documentation  

Failure to provide attribution may result in immediate termination of this license.

---

## Article 6 (Disclaimer of Warranty)
The Software is provided **“AS IS”**.  
The Licensor makes no express or implied warranties regarding the Software’s completeness, reliability, security, or fitness for a particular purpose.  
The User assumes all risks arising from use of the Software.

---

## Article 7 (Limitation of Liability)
The Licensor shall not be liable for any loss of data, system failure, financial damage, or indirect or consequential damages arising from use or inability to use the Software under any circumstances.

---

## Article 8 (Termination)
This Agreement remains valid while the User uses the Software.  
Violation of any terms allows POTENTIAL PROJECT™ to terminate this Agreement immediately.  
Upon termination, the User must immediately delete all copies of the Software.

---

## Article 9 (Governing Law)
This Agreement shall be governed by the laws of the **Republic of Korea**.  
In case of disputes, the **Seoul Central District Court** shall have exclusive jurisdiction.

---

## Article 10 (Contact)
For permissions, commercial inquiries, or requests to rename/change the infoUI object or logo, contact:  
📧 **jaeminan944@icloud.com**  
🌐 **https://github.com/Mercel4/Nightrol**

---

### Developer Summary
- ✅ Commercial use allowed  
- ✅ Must credit **“POTENTIAL PROJECT™”** in game or project  
- ✅ infoUI object name is fixed as **Nightrol™** (changes require permission)  
- ✅ infoUI logo is fixed as **Nightrol™ logo** (changes require permission)  
- ❌ Redistribution/sale as standalone software prohibited  
- ✅ Integration and modification within a Unity project allowed, but attribution must remain
