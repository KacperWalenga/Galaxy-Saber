using System;
using _GalaxySaber;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LaserSpawner laserSpawner;

    public static BeatMapInfo CurrentBeatMapInfo { get; private set; }
    public static BeatMap CurrentBeatMap { get; private set; }
    public static int currentDifficulty { get; private set; }

    public static void LoadMap(BeatMapInfo beatMapInfo, int difficultyIndex)
    {
        EventManager.TriggerEvent(Consts.Events.Game.Loading);
        
        var difficultyPath = beatMapInfo.DifficultySets[difficultyIndex].BeatMapFilename;
        var beatMap = BeatMapLoader.LoadBeatMap(difficultyPath);
        
        CurrentBeatMap = beatMap;
        CurrentBeatMapInfo = beatMapInfo;
        currentDifficulty = difficultyIndex;
        
        EventManager.TriggerEvent(Consts.Events.Game.Loaded);
    }
}
