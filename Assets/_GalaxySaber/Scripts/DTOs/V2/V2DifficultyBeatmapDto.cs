using System;
using Newtonsoft.Json;

[Serializable]
public class V2DifficultyBeatmapDto
{
    [JsonProperty("_difficulty")]
    public string Difficulty;

    [JsonProperty("_beatmapFilename")]
    public string BeatmapFilename;

    [JsonProperty("_noteJumpMovementSpeed")]
    public int NoteJumpMovementSpeed;

    [JsonProperty("_difficultyRank")]
    public int DifficultyRank;
}