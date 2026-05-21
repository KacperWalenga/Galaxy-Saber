# Galaxy Saber

**Galaxy Saber** is a VR rhythm game developed in Unity.  
The player uses a virtual lightsaber to hit incoming laser notes synchronized with music.

## Features

- VR gameplay with lightsaber interaction
- Dynamic song loading from an external `Maps` folder
- Support for multiple beatmap formats
- Music synchronization
- Laser spawning based on beatmap timing
- Score and health systems
- Simple UI flow for song selection, gameplay, and end screen

## Technologies

- Unity
- C#
- XR Interaction Toolkit
- JSON beatmap parsing

## Gameplay Flow

1. The player selects a song.
2. The game loads the beatmap and audio.
3. Lasers are spawned in sync with the music.
4. The player hits lasers using a lightsaber.
5. Score and health are updated during gameplay.
6. The result screen is shown after the song ends.

## Adding Songs

Songs can be added by placing them inside the external `Maps` folder.  
Each song should include audio, metadata, and beatmap files.
