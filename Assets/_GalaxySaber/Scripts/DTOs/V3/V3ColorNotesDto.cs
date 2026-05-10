using System;
using Newtonsoft.Json;

[Serializable]
public class V3ColorNotesDto
{
    [JsonProperty("b")]
    public float Beat;

    [JsonProperty("x")]
    public int LineIndex;

    [JsonProperty("y")]
    public int LineLayer;

    [JsonProperty("a")]
    public int color;

    [JsonProperty("c")]
    public int CutDirection;

    [JsonProperty("d")]
    public int AngleOffset;
}