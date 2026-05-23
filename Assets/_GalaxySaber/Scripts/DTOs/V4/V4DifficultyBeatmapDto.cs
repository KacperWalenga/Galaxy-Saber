using System;
using Newtonsoft.Json;

[Serializable]
public class V4DifficultyBeatmapDto
{
    [JsonProperty("difficulty")]
    public string Difficulty;

    [JsonProperty("beatmapAuthors")]
    public V4BeatmapAuthorsDto BeatmapAuthors;

    [JsonProperty("beatmapDataFilename")]
    public string BeatmapDataFilename;

    [JsonProperty("noteJumpMovementSpeed")]
    public int NoteJumpMovementSpeed;
}