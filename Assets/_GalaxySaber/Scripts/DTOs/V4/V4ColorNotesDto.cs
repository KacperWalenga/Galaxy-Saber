using System;
using Newtonsoft.Json;

[Serializable]
public class V4ColorNotesDto
{
    [JsonProperty("b")]
    public float Beat;

    [JsonProperty("r")]
    public float RotationLane;

    [JsonProperty("i")]
    public int MetadataIndex;
}