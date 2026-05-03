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
            difficultySets: MapDifficulties(dto.DifficultyBeatmapSets)
        );
    }

    private static DifficultySet[] MapDifficulties(V2DifficultyBeatmapSetDto[] sets)
    {
        if (sets == null || sets.Length == 0)
            return Array.Empty<DifficultySet>();

        return sets
            .Where(x => x?.DifficultyBeatmaps != null)
            .SelectMany(x => x.DifficultyBeatmaps)
            .Where(x => x != null && !string.IsNullOrWhiteSpace(x.BeatmapFilename))
            .Select(x => new DifficultySet(
                x.Difficulty ?? string.Empty,
                x.BeatmapFilename
            ))
            .ToArray();
    }
}