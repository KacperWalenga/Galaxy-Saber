using System;
using Newtonsoft.Json;

[Serializable]
public class V2DifficultyBeatmapSetDto
{
    [JsonProperty("_beatmapCharacteristicName")]
    public string CharacteristicName;

    [JsonProperty("_difficultyBeatmaps")]
    public V2DifficultyBeatmapDto[] DifficultyBeatmaps;
}