using System;
using System.Collections.Generic;
using System.IO;
using com.cyborgAssets.inspectorButtonPro;
using Newtonsoft.Json;
using UnityEngine;

public class BeatMapLoader : MonoBehaviour
{
    public static List<BeatMapInfo> BeatMapsInfo { get; private set; } = new();

    private void OnDestroy()
    {
        BeatMapsInfo.Clear();
    }

    [ProButton]
    public static void LoadBeatMapsInfos()
    {
        var folder = Path.Combine(Application.dataPath, "../Maps");

        if (!Directory.Exists(folder))
        {
            Debug.LogError($"BeatMaps folder doesn't exist: {folder}");
            return;
        }

        var mapPaths = Directory.GetDirectories(folder);

        foreach (var mapPath in mapPaths)
        {
            var infoDatFile = Path.Combine(mapPath, "info.dat");

            if (!File.Exists(infoDatFile))
            {
                Debug.LogWarning($"Skipping map, info.dat not found: {infoDatFile}");
                continue;
            }

            var json = File.ReadAllText(infoDatFile);

            try
            {
                var beatMap = BeatMapParser.ParseBeatMapInfo(json, mapPath);
                Debug.Log(
                    $"Loaded BeatMap: {beatMap.SongName} | v{beatMap.Version}"
                    + JsonConvert.SerializeObject(beatMap, Formatting.Indented)
                );
                BeatMapsInfo.Add(beatMap);
            }
            catch (UnsupportedBeatMapVersionException e)
            {
                Debug.LogWarning($"Skipping map: {mapPath}\n{e.Message}");
            }
            catch (InvalidBeatMapException e)
            {
                Debug.LogError($"Invalid map: {mapPath}\n{e}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Unexpected error while loading map: {mapPath}\n{e}");
            }
        }
    }

    public static BeatMap LoadBeatMap(string difficultyPath)
    {
        var json = File.ReadAllText(difficultyPath);
        return BeatMapParser.ParseBeatMap(json);
    }
}