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
        EventManager.StartListening(Consts.Events.Game.Lost, GameLost);
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
        var gameLength = audioClip.length;

        audioSource.clip = audioClip;
        laserSpawner.Init(beatMap.BeatData, speed, beatMapInfo.BeatsPerMinute);
        audioSource.Play();
        laserSpawner.StartSpawning();
        
        Invoke(nameof(EndGame), gameLength);
        
        EventManager.TriggerEvent(Consts.Events.Game.Started);
    }

    private void EndGame()
    {
        audioSource.Stop();
        
        EventManager.TriggerEvent(Consts.Events.Game.Ended);
    }

    private void GameLost()
    {
        audioSource.Stop();
        laserSpawner.StopSpawning();
        laserSpawner.lasersPool.DestroyAllLasers();
        
        EventManager.TriggerEvent(Consts.Events.Game.Ended);
    }
}
