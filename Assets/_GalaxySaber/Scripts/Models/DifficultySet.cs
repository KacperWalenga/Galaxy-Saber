using System;

[Serializable]
public class DifficultySet
{
    public string Difficulty;
    public string BeatMapFilename;
    
    public DifficultySet(
        string difficulty,
        string beatMapFilename
        )
    {
        Difficulty = difficulty;
        BeatMapFilename = beatMapFilename;
    }
}
