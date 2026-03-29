using System;
using Newtonsoft.Json;

[Serializable]
public class V4BeatmapAuthorsDto
{
    [JsonProperty("mappers")]
    public string[] Mappers;

    [JsonProperty("lighters")]
    public string[] Lighters;
}