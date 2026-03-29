using System;

[Serializable]
public class BeatMapInfo
{
    public string Version;
    public string SongName;
    public string SongSubName;
    public string SongAuthor;
    public string LevelAuthorName;
    public float BeatsPerMinute;
    public string CoverImageFilename;
    public string SongFileName;
    public DifficultySet[] DifficultySets;
    
    public BeatMapInfo(
        string version,
        string songName,
        string songSubName,
        string songAuthor,
        string levelAuthorName,
        float beatsPerMinute,
        string coverImageFilename,
        string songFileName,
        DifficultySet[] difficultySets)
    {
        Version = version;
        SongName = songName;
        SongSubName = songSubName;
        SongAuthor = songAuthor;
        LevelAuthorName = levelAuthorName;
        BeatsPerMinute = beatsPerMinute;
        CoverImageFilename = coverImageFilename;
        SongFileName = songFileName;
        DifficultySets = difficultySets;
    }
}
