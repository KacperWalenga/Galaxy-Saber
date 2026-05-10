using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class BeatMapV4Mapper
{
    public static BeatMap ToDomain(BeatMapV4Dto dto)
    {
        if (dto == null)
            throw new InvalidBeatMapException("BeatMapInfoV4Dto is null");

        var bdList = new List<BeatData>();
        
        foreach (var note in dto.ColorNotes)
        {
            var colorNoteData = dto.ColorNotesData[note.MetadataIndex];
            var beatData = new BeatData(note.Beat, colorNoteData.LineIndex, colorNoteData.LineLayer);
            bdList.Add(beatData);
        }
        
        return new BeatMap(
            beatData: bdList
        );
    }
}