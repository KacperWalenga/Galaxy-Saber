using System;
using Newtonsoft.Json;

[Serializable]
public class V4SongDto
{
    [JsonProperty("title")]
    public string Title;

    [JsonProperty("subTitle")]
    public string SubTitle;

    [JsonProperty("author")]
    public string Author;
}