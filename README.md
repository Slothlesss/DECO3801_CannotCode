# DECO3801_CannotCode
<p align="center">
    <img src="https://github.com/user-attachments/assets/d3fee8bb-6340-4fee-b9f2-c201fcfd1047" />
</p>
        
## Welcome to "Mind Over Matter"
Mind Over Matter is a brain-computer interaction (BCI) powered 2D side scroller game in which players control a spaceship and must fly as far as possible without crashing. The player will have to avoid procedurally generated obstacles including asteroids, moons, planets and other spaceships. Certain obstacles can also be shot by the laser cannons on the player’s ship to destroy them. The player’s ship will be controlled using the traditional paradigm of a keyboard, however the game’s environment will automatically adapt to the user’s cognitive state based on data sent by an EEG (electroencephalogram) headset.

## Table Of Contents
- [Getting Started](#getting-started)
    + [1. Setup Unity](#1-setup-unity)
    + [2. Run server.py file](#2-run-server.py-file)
    + [3. Setup Headset](#3-setup-headset)
    + [4. Start the game](#4-start-the-game)
- [How To Play](#how-to-play)
    + [1. Player Mode Selection](#1-player-mode-selection)
    + [2. Player Movement](#2-player-movement)
- [Lisence](#license)


## Getting Started
### 1. Setup Unity
- The Unity Editor is required. Download it from <a href="https://unity.com/download">Unity3d.com/get-unity/download</a>.
- Download the project via
```bash  
git clone https://github.com/Slothlesss/DECO3801_CannotCode
```
- Open Unity Editor.
- Click Add -> Add project from disk
- Navigate to the cloned project folder `./DECO3801_CannotCode/DECO3801`
- Open it using Unity version 2022.3.17f1 or higher.
![image](https://github.com/user-attachments/assets/3ac798d2-8da6-4476-bb60-f7be6ddbb0f7)



### 2. Setup headset
- Download the Emotiv Launcher from https://www.emotiv.com/pages/download-emotiv-launcher
- Sign in with EmotivID: cannotcode1 and passsword: Cannotcode11
- Wear headset by instruction.
- Click "Connect" until you reach "Close".

### 3. Transmit data to Unity
- Navigate to `./DECO3801_CannotCode/Asset/EEG/server.py`.
- Open the file using Visual Studio Code (or any Python-supporting IDE).
- Right-click the file tab → select "Reveal in File Explorer" (Windows) or "Reveal in Finder" (Mac).
- In the File Explorer/Finder window, copy the path.
- For windows: In the terminal, run:

   ```
   pip install -r requirements.txt
   cd "D:\Project\GitHub\DECO3801_CannotCode\DECO3801\Assets\EEG
   python server.py
   ```
- For MacOS: open EEG folder in terminal, then run:

    ```
    pip3 install -r requirements.txt
    cd '' && '/usr/local/bin/python3'  'server.py'  && echo Exit status: $? && exit 1
    ```

- If its the first time connecting, Click "Approve" in Emotiv Launcher.

### 4. Start the game
- Navigate to `./DECO3801_CannotCode/Asset/Scenes/SampleScene`.
- Double-click `SampleScene`.
- Click the Play button at the top of the Unity Editor to start the game.

## 🎮 How To Play
### 1. Player Mode Selection
Players can choose between 1 Player or 2 Player mode using left/right buttons.
<p align="center">
  <img src="https://github.com/user-attachments/assets/efc91298-da52-4af3-8be7-9e4a2993b713" width="900"/>
</p>

### 2. Player Movement
- Player 1 (Left side)
    + Go Up: A
    + Go Down: W
- Player 2 (Right side)
    + Go Up: ↑ (Up Arrow)
    + Go Down: ↓ (Down Arrow)
 
### 3. Obstacles
- Asteroid
<p>
  <img src="https://github.com/user-attachments/assets/f5a4b15a-497c-4815-86fc-4ceb375ab432" width="150" height="150"/>
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://github.com/user-attachments/assets/ea8f8c4c-844e-4b0c-a9c4-bc5492276d26" width="150" height="150"/>
</p>


- Asteroid Group
<p>
    <img src="https://github.com/user-attachments/assets/40e879d2-2a21-48f3-8b6a-37c63741df2a" width="200"/>
</p>

- Planet
<p>
    <img src="https://github.com/user-attachments/assets/607e186c-6a0e-42fe-a651-6be9e372abc1" width="300"/>
</p>

### 4. Adaptive Gameplay
<p>
  <img src="https://github.com/user-attachments/assets/5884ad61-f67f-4583-b8ed-58e8ce794e82" width="500"/>
</p>

The headset will detect your level of frustration, fatigue and focus:
- Frustration: Increases game difficulty. The more frustrated you are, the harder the game becomes => so try to stay calm.
- Fatigue: Triggers additional obstacles. Higher fatigue levels introduce more challenges in the environment.
- Focus: Unlocks and strengthens your weapon. The more focused you are, the more powerful your weapon becomes.

## License
Current assets used:
- Talia's arts.
- Free UI Hologram Interface - https://wenrexa.itch.io/holoui
- Assets Free Laser Bullets Pack 2020 - https://wenrexa.itch.io/laser2020

All 3rd-party assets and libraries used in this project retain all rights under their respective licenses.
