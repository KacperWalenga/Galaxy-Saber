using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class BeatMapV2Dto
{
    [JsonProperty("_notes")]
    public List<V2NotesDto> Notes;

    public BeatMap ToModel()
    {
        return BeatMapV2Mapper.ToDomain(this);
    }
}