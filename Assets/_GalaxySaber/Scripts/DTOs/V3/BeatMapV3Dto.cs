using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class BeatMapV3Dto
{
    [JsonProperty("colorNotes")]
    public List<V3ColorNotesDto> ColorNotes;

    public BeatMap ToModel()
    {
        return BeatMapV3Mapper.ToDomain(this);
    }
}