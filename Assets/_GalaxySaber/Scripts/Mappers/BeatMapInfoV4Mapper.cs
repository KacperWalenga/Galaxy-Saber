using System;
using System.Linq;

public static class BeatMapInfoV4Mapper
{
    public static BeatMapInfo ToDomain(BeatMapInfoV4Dto dto)
    {
        if (dto == null)
            throw new InvalidBeatMapException("BeatMapInfoV4Dto is null");

        var levelAuthorName = dto.DifficultyBeatmaps?
            .SelectMany(x => x?.BeatmapAuthors?.Mappers ?? Array.Empty<string>())
            .FirstOrDefault() ?? string.Empty;

        return new BeatMapInfo(
            version: dto.Version ?? string.Empty,
            songName: dto.Song?.Title ?? string.Empty,
            songSubName: dto.Song?.SubTitle ?? string.Empty,
            songAuthor: dto.Song?.Author ?? string.Empty,
            levelAuthorName: levelAuthorName,
            beatsPerMinute: dto.Audio?.Bpm ?? 0f,
            coverImageFilename: dto.CoverImageFilename ?? string.Empty,
            songFileName: dto.Audio?.SongFilename ?? string.Empty,
            difficultySets: MapDifficulties(dto.DifficultyBeatmaps)
        );
    }

    private static DifficultySet[] MapDifficulties(V4DifficultyBeatmapDto[] beatmaps)
    {
        if (beatmaps == null || beatmaps.Length == 0)
            return Array.Empty<DifficultySet>();

        return beatmaps
            .Where(x => x != null && !string.IsNullOrWhiteSpace(x.BeatmapDataFilename))
            .Select(x => new DifficultySet(
                x.Difficulty ?? string.Empty,
                x.BeatmapDataFilename
            ))
            .ToArray();
    }
}