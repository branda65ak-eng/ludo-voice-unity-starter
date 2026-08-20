# Ludo + Voice (Unity starter)

This repository contains a minimal starter scaffold for a Ludo-style multiplayer game in Unity with Photon networking hooks and Photon Voice integration notes. It's a minimal starting point to run a local board and connect to Photon rooms. The project intentionally contains only essential scripts and instructions — not a full Unity binary.

Quick start

1. Clone the repo:

   git clone https://github.com/branda65ak-eng/ludo-voice-unity-starter.git

2. Open Unity (2020.3 LTS or newer). Create a new empty project and copy the Assets/ folder from this repository into your project, or open the repo in Unity if you prefer.

3. Import Photon PUN 2 (Free) from the Asset Store or Photon website. Import Photon Voice 2 to enable voice chat.

4. Configure Photon AppId:
   - Create an account at https://dashboard.photonengine.com and create an app (PUN and/or Voice).
   - In Unity, open Assets/Photon/PhotonUnityNetworking/Resources/PhotonServerSettings and paste your AppId.

5. Scene setup (minimal):
   - Create an empty GameObject named "Board" and add BoardManager. Create tile child objects and assign them to the BoardManager.tiles array in order.
   - Create Piece prefabs and place one per player in the scene. Attach the Piece script.
   - Create an empty GameObject "GameManager" and attach GameManager. Assign BoardManager, Dice (a GameObject with Dice.cs), and playerPieces list.
   - Create a UI canvas with a Roll button wired to GameManager.OnRollButtonPressed and basic room create/join buttons calling NetworkManager.CreateOrJoinRoom().
   - Add NetworkManager to a GameObject so it connects to Photon on Start.

Photon Voice quick notes

- After importing Photon Voice, add VoiceConnection prefab and configure AppId.
- Add Recorder component on the local player prefab and Speaker component to the remote player objects to route audio.
- Toggle Recorder.TransmitEnabled to mute/unmute local mic.

What's included

- Assets/Scripts/BoardManager.cs
- Assets/Scripts/Piece.cs
- Assets/Scripts/Dice.cs
- Assets/Scripts/GameManager.cs
- Assets/Scripts/NetworkManager.cs
- README, .gitignore, LICENSE

Next steps I can do for you

- Push a .unitypackage with scene + prefabs and basic tile objects so you can open and run directly.
- Expand GameManager for full Ludo rules (4 pieces per player, home/goal, capture logic).
- Add UI, matchmaking UI, and Photon room property persistence.

License

MIT — see LICENSE file.
