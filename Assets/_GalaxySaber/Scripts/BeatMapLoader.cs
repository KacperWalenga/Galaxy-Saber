using System.IO;
using com.cyborgAssets.inspectorButtonPro;
using Newtonsoft.Json;
using UnityEngine;

public class BeatMapLoader : MonoBehaviour
{
    [ProButton]
    public void GetBeatMaps()
    {
        var folder = Path.Combine(Application.dataPath, "../Maps");

        if (!Directory.Exists(folder))
        {
            Debug.LogError($"BeatMaps folder doesn't exist: {folder}", this);
            return;
        }

        var maps = Directory.GetDirectories(folder);

        foreach (var map in maps)
        {
            var infoDatFile = Path.Combine(map, "info.dat");

            if (!File.Exists(infoDatFile))
            {
                Debug.LogWarning($"Skipping map, info.dat not found: {infoDatFile}", this);
                continue;
            }

            var json = File.ReadAllText(infoDatFile);

            try
            {
                var beatMap = BeatMapParser.Parse(json);
                Debug.Log(
                    $"Loaded BeatMap: {beatMap.SongName} | v{beatMap.Version}"
                    + JsonConvert.SerializeObject(beatMap, Formatting.Indented)
                );
            }
            catch (UnsupportedBeatMapVersionException e)
            {
                Debug.LogWarning($"Skipping map: {map}\n{e.Message}", this);
            }
            catch (InvalidBeatMapException e)
            {
                Debug.LogError($"Invalid map: {map}\n{e}", this);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Unexpected error while loading map: {map}\n{e}", this);
            }
        }
    }
}