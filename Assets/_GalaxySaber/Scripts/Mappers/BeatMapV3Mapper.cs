using System.Collections.Generic;
using UnityEngine;

public static class BeatMapV3Mapper
{
    public static BeatMap ToDomain(BeatMapV3Dto dto)
    {
        if (dto == null)
            throw new InvalidBeatMapException("BeatMapInfoV3Dto is null");

        var bdList = new List<BeatData>();

        Debug.Log(dto.ColorNotes);
        
        foreach (var note in dto.ColorNotes)
        {
            var beatData = new BeatData(note.Beat, note.LineIndex, note.LineLayer);
            bdList.Add(beatData);
        }
        
        return new BeatMap(
            beatData: bdList
        );
    }
}