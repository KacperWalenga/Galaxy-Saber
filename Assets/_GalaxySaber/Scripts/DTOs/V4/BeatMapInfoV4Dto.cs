using System;
using Newtonsoft.Json;

[Serializable]
public class BeatMapInfoV4Dto : IBeatMapInfoDto
{
    [JsonProperty("version")]
    public string Version;

    [JsonProperty("song")]
    public V4SongDto Song;

    [JsonProperty("audio")]
    public V4AudioDto Audio;

    [JsonProperty("coverImageFilename")]
    public string CoverImageFilename;

    [JsonProperty("difficultyBeatmaps")]
    public V4DifficultyBeatmapDto[] DifficultyBeatmaps;

    public BeatMapInfo ToModel(string mapPath)
    {
        return BeatMapInfoV4Mapper.ToDomain(this, mapPath);
    }
}