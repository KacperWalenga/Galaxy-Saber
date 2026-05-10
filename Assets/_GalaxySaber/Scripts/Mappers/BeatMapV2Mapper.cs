using System.Collections.Generic;

public static class BeatMapV2Mapper
{
    public static BeatMap ToDomain(BeatMapV2Dto dto)
    {
        if (dto == null)
            throw new InvalidBeatMapException("BeatMapInfoV2Dto is null");

        var bdList = new List<BeatData>();
        
        foreach (var note in dto.Notes)
        {
            var beatData = new BeatData(note.Time, note.LineIndex, note.LineLayer);
            bdList.Add(beatData);
        }
        
        return new BeatMap(
            beatData: bdList
        );
    }
}