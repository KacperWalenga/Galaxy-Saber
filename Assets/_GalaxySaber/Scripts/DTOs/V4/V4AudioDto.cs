using System;
using Newtonsoft.Json;

[Serializable]
public class V4AudioDto
{
    [JsonProperty("songFilename")]
    public string SongFilename;

    [JsonProperty("songDuration")]
    public float SongDuration;

    [JsonProperty("audioDataFilename")]
    public string AudioDataFilename;

    [JsonProperty("bpm")]
    public float Bpm;
}