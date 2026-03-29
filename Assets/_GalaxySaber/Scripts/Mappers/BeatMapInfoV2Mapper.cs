using System;
using System.Linq;

public static class BeatMapInfoV2Mapper
{
    public static BeatMapInfo ToDomain(BeatMapInfoV2Dto dto)
    {
        if (dto == null)
            throw new InvalidBeatMapException("BeatMapInfoV2Dto is null");

        return new BeatMapInfo(
            version: dto.Version ?? string.Empty,
            songName: dto.SongName ?? string.Empty,
            songSubName: dto.SongSubName ?? string.Empty,
            songAuthor: dto.SongAuthor ?? string.Empty,
            levelAuthorName: dto.LevelAuthorName ?? string.Empty,
            beatsPerMinute: dto.BeatsPerMinute,
            coverImageFilename: dto.CoverImageFilename ?? string.Empty,
            songFileName: dto.SongFileName ?? string.Empty,
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