using System;
using System.IO;
using System.Linq;

public static class BeatMapInfoV2Mapper
{
    public static BeatMapInfo ToDomain(BeatMapInfoV2Dto dto, string mapPath)
    {
        if (dto == null)
            throw new InvalidBeatMapException("BeatMapInfoV2Dto is null");

        var songPath = Path.Combine(mapPath, dto.CoverImageFilename);
        var imagePath = Path.Combine(mapPath, dto.SongFileName);
        
        return new BeatMapInfo(
            version: dto.Version ?? string.Empty,
            songName: dto.SongName ?? string.Empty,
            songSubName: dto.SongSubName ?? string.Empty,
            songAuthor: dto.SongAuthor ?? string.Empty,
            levelAuthorName: dto.LevelAuthorName ?? string.Empty,
            beatsPerMinute: dto.BeatsPerMinute,
            coverImageFilename: songPath,
            songFileName: imagePath,
            duration: 0,
            difficultySets: MapDifficulties(dto.DifficultyBeatmapSets, mapPath)
        );
    }

    private static DifficultySet[] MapDifficulties(
        V2DifficultyBeatmapSetDto[] sets,
        string mapPath
    )
    {
        if (sets == null || sets.Length == 0)
            return Array.Empty<DifficultySet>();

        return sets
            .Where(set => set?.DifficultyBeatmaps != null)
            .SelectMany(set => set.DifficultyBeatmaps)
            .Where(beatmap =>
                beatmap != null &&
                !string.IsNullOrWhiteSpace(beatmap.BeatmapFilename))
            .Select(beatmap =>
            {
                var beatmapPath = Path.Combine(mapPath, beatmap.BeatmapFilename);

                var notesCount = BeatMapNoteCounter.CountNotes(beatmapPath);

                return new DifficultySet(
                    beatmap.Difficulty ?? string.Empty,
                    beatmap.BeatmapFilename,
                    int.Parse(beatmap.NoteJumpMovementSpeed),
                    notesCount
                );
            })
            .ToArray();
    }
}