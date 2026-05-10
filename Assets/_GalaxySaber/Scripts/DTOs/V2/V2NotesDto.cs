using System;
using Newtonsoft.Json;

[Serializable]
public class V2NotesDto
{
    [JsonProperty("_time")]
    public float Time;

    [JsonProperty("_lineIndex")]
    public int LineIndex;

    [JsonProperty("_lineLayer")]
    public int LineLayer;

    [JsonProperty("_type")]
    public int Type;

    [JsonProperty("_cutDirection")]
    public int CutDirection;
}