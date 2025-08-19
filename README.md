# 🌲 Forest Rescue 🪓  
Forest rescue is a 3D platforming / action-adventure game set in a fantasy forest, where you need to defeat goblins and rescue animals! 
<div align="center">
  <img src="/Documents/FinalReport/OtherImages/GameImage.png" alt="Game Image" width="60%">
</div>


## Contents
- [How to Download And Play](#how-to-download-and-play-windows-devices-only)
- [How to run in the Unity Editor](#how-to-run-in-the-unity-editor)
- [Share Your Feedback](#share-your-feedback)
- [Directory Structure and Code Documentation](#directory-structure-and-code-documentation)

## How to Download and Play (Windows Devices Only)

1. Download the **[Latest Release](https://gitlab.cim.rhul.ac.uk/zkac336/PROJECT/-/releases)**.
2. Unzip the file, and the Forest Rescue application will be inside.
---
### Game Controls:
You can connect a controller to your PC via bluetooth or a wired connection and it will automatically connect to the game when running.
| Action          | Keyboard       | Game Controller        |
|------------------|-------------------------|---------------------------|
| Move            | W/A/S/D                 | Left Stick             |
| Jump            | Spacebar                | Button South                  |
| Run             | Left Shift (Hold)       | Left Shoulder (Hold)   |
| Attack          | Left Mouse Button       | Button West                  |


## How to Run in the Unity Editor
1. Download the latest version of the Unity Hub
2. Clone this repository, and open the FinalProject folder in the Unity Editor. 

### System Requirements:
Unity Hub system requirements:
OS: Windows 7 SP1+, 8, 10, 64-bit versions only; Mac OS X 10.13+; Ubuntu 16.04, 18.04, and CentOS 7.
GPU:Graphics card with DX10 (shader model 4.0) capabilities.


## Share Your Feedback 
Leave feedback here : ➡️ **[User Feedback Form](https://docs.google.com/forms/d/e/1FAIpQLSfYDNJzHmAWeYxdV7bLPPbsJIW2R6UqZe9Kt2eWmDSAP7TOhg/viewform)** <br>
You can also report any bugs you find here: ➡️ **[Bug Report Form](https://docs.google.com/forms/d/e/1FAIpQLSeGxXRMbLGKBaWU_mlh32TSN13TTJqoxCiIrTSPRz3m5WpXHw/viewform?usp=header)**


---

## Directory Structure and Code Documentation
You can find the full project documentation in [Documents/HtmlDocumentation/index.html](Documents/HtmlDocumentation/index.html)

### Directory Structure

<div align="center">
  <img src="/Documents/FinalReport/OtherImages/FileStructure.png" alt="Game Image" width="30%">
</div>

The `Assets` folder contains all of the work completed so far including the 3D models for the player, enemies, terrain etc., as well as the associated scripts and animations. The diagram highlights the most important subfolders, such as the scripts folders, where all the C# scripts are organised.  
Note that not all folders are included in the diagram.

---

### Files Relevant to the Player State Machine:

- [`PlayerBaseState.cs`](FinalProject/Assets/Fighter/Scripts/PlayerBaseState.cs), [`PlayerRunState.cs`](FinalProject/Assets/Fighter/Scripts/PlayerRunState.cs), [`PlayerJumpState.cs`](FinalProject/Assets/Fighter/Scripts/PlayerJumpState.cs), [`PlayerStateMachine.cs`](FinalProject/Assets/Fighter/Scripts/PlayerStateMachine.cs), [`PlayerStateFactory.cs`](FinalProject/Assets/Fighter/Scripts/PlayerStateFactory.cs)  
  *(There are other state files within this folder — Jump and Run are just two examples.)*

---

### Files Relevant to the Combat System:

- [`IDamageable.cs`](FinalProject/Assets/Interfaces/IDamageable.cs)
- [`PlayerHealthAndDamage.cs`](FinalProject/Assets/Fighter/Scripts/PlayerHealthAndDamage.cs), [`WeaponManager.cs`](FinalProject/Assets/Fighter/Scripts/WeaponManager.cs), [`WeaponAttributes.cs`](FinalProject/Assets/Fighter/Scripts/WeaponAttributes.cs)
- [`Enemy.cs`](FinalProject/Assets/CuteGoblins/Scripts/Enemy.cs), [`AttackRadius.cs`](FinalProject/Assets/CuteGoblins/Scripts/AttackRadius.cs), [`RangedAttackRadius.cs`](FinalProject/Assets/CuteGoblins/Scripts/RangedAttackRadius.cs), [`ObjectPool.cs`](FinalProject/Assets/CuteGoblins/Scripts/ObjectPool.cs), [`PoolableObject.cs`](FinalProject/Assets/CuteGoblins/Scripts/PoolableObject.cs), [`Arrow.cs`](FinalProject/Assets/CuteGoblins/Scripts/Arrow.cs)

---

### Files Relevant to Enemy Movement:

- [`Enemy.cs`](FinalProject/Assets/CuteGoblins/Scripts/Enemy.cs), [`FollowRadius.cs`](FinalProject/Assets/CuteGoblins/Scripts/FollowRadius.cs), [`EnemyMovement.cs`](FinalProject/Assets/CuteGoblins/Scripts/EnemyMovement.cs), [`EnemyScriptableObject.cs`](FinalProject/Assets/CuteGoblins/Scripts/EnemyScriptableObject.cs), [`RangedEnemyMovement.cs`](FinalProject/Assets/CuteGoblins/Scripts/RangedEnemyMovement.cs)

---

### Files Relevant to the Health UI System:

- [`IHealthSubject.cs`](FinalProject/Assets/Interfaces/IHealthSubject.cs), [`IHealthObserver.cs`](FinalProject/Assets/Interfaces/IHealthObserver.cs)
- [`PlayerHealthBar.cs`](FinalProject/Assets/UI/Scripts/PlayerHealthBar.cs), [`EnemyHealthBar.cs`](FinalProject/Assets/UI/Scripts/EnemyHealthBar.cs), [`GameOver.cs`](FinalProject/Assets/UI/Scripts/GameOver.cs)

---

### Files Relevant to Animal Rescue:

- [`Cage.cs`](FinalProject/Assets/CuteRaccoons/Scripts/Cage.cs)
- [`Animal.cs`](FinalProject/Assets/Interfaces/Animal.cs), [`AnimalTeleportRadius.cs`](FinalProject/Assets/Interfaces/AnimalTeleportRadius.cs)

---

### Files Relevant to Level Progression and Score System:

- [`UIQuestManager.cs`](FinalProject/Assets/CuteRaccoons/Scripts/UIQuestManager.cs)
- [`LevelManager.cs`](FinalProject/Assets/Managers/LevelManager.cs), [`LevelState.cs`](FinalProject/Assets/Managers/LevelState.cs)
- [`GoalRadius.cs`](FinalProject/Assets/UI/Scripts/GoalRadius.cs), [`LevelCleared.cs`](FinalProject/Assets/UI/Scripts/LevelCleared.cs), [`GameTimer.cs`](FinalProject/Assets/UI/Scripts/GameTimer.cs), [`ScoreManager.cs`](FinalProject/Assets/UI/Scripts/ScoreManager.cs), [`SceneLoader.cs`](FinalProject/Assets/UI/Scripts/SceneLoader.cs)

---

### Files Relevant to the Sound System:

- [`SoundFXManager.cs`](FinalProject/Assets/Managers/SoundFXManager.cs), [`SoundMixerManager.cs`](FinalProject/Assets/Managers/SoundMixerManager.cs)

---

### Test Files:

- [`LevelManagerTests.cs`](FinalProject/Assets/Tests/EditMode/LevelManagerTests.cs), [`PlayerStateFactoryTests.cs`](FinalProject/Assets/Tests/EditMode/PlayerStateFactoryTests.cs)
