using System;
using _GalaxySaber;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LaserSpawner laserSpawner;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        EventManager.StartListening(Consts.Events.Game.Loaded, InitGame);
    }

    private void InitGame()
    {
        StartGame();
    }

    private async Awaitable StartGame()
    {
        var beatMap = GameLoader.CurrentBeatMap;
        var beatMapInfo = GameLoader.CurrentBeatMapInfo;
        var difficulty = GameLoader.currentDifficulty;
        var speed = beatMapInfo.DifficultySets[difficulty].NoteJumpMovementSpeed;
        var audioClip = await AudioLoader.LoadAudioClip(beatMapInfo.SongFileName);

        audioSource.clip = audioClip;
        laserSpawner.Init(beatMap.BeatData, speed);
        
        audioSource.Play();
        laserSpawner.StartSpawning();
    }
}
