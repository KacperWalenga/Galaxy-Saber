using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class BeatMapV4Dto
{
    [JsonProperty("colorNotes")]
    public List<V4ColorNotesDto>  ColorNotes;

    [JsonProperty("colorNotesData")]
    public List<V4ColorNotesDataDto> ColorNotesData;

    public BeatMap ToModel()
    {
        return BeatMapV4Mapper.ToDomain(this);
    }
}