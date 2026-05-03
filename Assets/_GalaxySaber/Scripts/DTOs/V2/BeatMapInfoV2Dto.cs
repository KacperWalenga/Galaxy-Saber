using System;
using Newtonsoft.Json;

[Serializable]
public class BeatMapInfoV2Dto : IBeatMapInfoDto
{
    [JsonProperty("_version")]
    public string Version;

    [JsonProperty("_songName")]
    public string SongName;

    [JsonProperty("_songSubName")]
    public string SongSubName;

    [JsonProperty("_songAuthorName")]
    public string SongAuthor;

    [JsonProperty("_levelAuthorName")]
    public string LevelAuthorName;

    [JsonProperty("_beatsPerMinute")]
    public float BeatsPerMinute;

    [JsonProperty("_coverImageFilename")]
    public string CoverImageFilename;

    [JsonProperty("_songFilename")]
    public string SongFileName;

    [JsonProperty("_difficultyBeatmapSets")]
    public V2DifficultyBeatmapSetDto[] DifficultyBeatmapSets;

    public BeatMapInfo ToModel(string path)
    {
        return BeatMapInfoV2Mapper.ToDomain(this, path);
    }
}