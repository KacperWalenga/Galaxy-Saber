using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class BeatMapInfoV4Mapper
{
    public static BeatMapInfo ToDomain(BeatMapInfoV4Dto dto, string mapPath)
    {
        if (dto == null)
            throw new InvalidBeatMapException("BeatMapInfoV4Dto is null");

        var levelAuthorName = dto.DifficultyBeatmaps?
            .SelectMany(x => x?.BeatmapAuthors?.Mappers ?? Array.Empty<string>())
            .FirstOrDefault() ?? string.Empty;

        var songPath = Path.Combine(mapPath, dto.Audio.SongFilename);
        var imagePath = Path.Combine(mapPath, dto.CoverImageFilename);

        return new BeatMapInfo(
            version: dto.Version ?? string.Empty,
            songName: dto.Song?.Title ?? string.Empty,
            songSubName: dto.Song?.SubTitle ?? string.Empty,
            songAuthor: dto.Song?.Author ?? string.Empty,
            levelAuthorName: levelAuthorName,
            beatsPerMinute: dto.Audio?.Bpm ?? 0f,
            coverImageFilename: imagePath,
            songFileName: songPath,
            duration: dto.Audio?.SongDuration ?? 0,
            difficultySets: MapDifficulties(dto.DifficultyBeatmaps, mapPath)
        );
    }

    private static DifficultySet[] MapDifficulties(
        V4DifficultyBeatmapDto[] beatmaps,
        string mapPath
    )
    {
        if (beatmaps == null || beatmaps.Length == 0)
            return Array.Empty<DifficultySet>();

        return beatmaps
            .Where(beatMap =>
                beatMap != null &&
                !string.IsNullOrWhiteSpace(beatMap.BeatmapDataFilename))
            .Select(beatMap =>
            {
                var beatmapPath = Path.Combine(mapPath, beatMap.BeatmapDataFilename);

                var notesCount = BeatMapNoteCounter.CountNotes(beatmapPath);

                return new DifficultySet(
                    beatMap.Difficulty ?? string.Empty,
                    beatmapPath,
                    beatMap.NoteJumpMovementSpeed,
                    notesCount
                );
            })
            .ToArray();
    }
}