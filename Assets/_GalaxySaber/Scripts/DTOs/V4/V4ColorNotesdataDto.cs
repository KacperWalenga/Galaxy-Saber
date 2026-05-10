using System;
using Newtonsoft.Json;

[Serializable]
public class V4ColorNotesDataDto
{
    [JsonProperty("x")]
    public int LineIndex;

    [JsonProperty("y")]
    public int LineLayer;

    [JsonProperty("c")]
    public int Color;

    [JsonProperty("d")]
    public int CutDirection;

    [JsonProperty("a")]
    public int AngleOffset;
}