using System;
using System.IO;
using File = System.IO.File;

public static class SongMetadataReader
{
    public static float GetSongDuration(string songPath)
    {
        if (string.IsNullOrWhiteSpace(songPath))
            throw new ArgumentException("Song path is null or empty");

        if (!File.Exists(songPath))
            throw new FileNotFoundException($"Song file not found: {songPath}");

        using var file = songPath.EndsWith(".egg", StringComparison.OrdinalIgnoreCase)
            ? TagLib.File.Create(songPath, "taglib/ogg", TagLib.ReadStyle.Average)
            : TagLib.File.Create(songPath);

        return (float)file.Properties.Duration.TotalSeconds;
    }

    public static string GetFormattedSongDuration(string songPath)
    {
        var seconds = GetSongDuration(songPath);
        return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
    }
}