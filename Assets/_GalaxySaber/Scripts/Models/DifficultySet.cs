using System;

[Serializable]
public class DifficultySet
{
    public string Difficulty;
    public string BeatMapFilename;
    public int NoteJumpMovementSpeed;
    public int NotesCount;
    
    public DifficultySet(
        string difficulty,
        string beatMapFilename,
        int noteJumpMovementSpeed,
        int notesCount
        )
    {
        Difficulty = difficulty;
        beatMapFilename = beatMapFilename;
        NoteJumpMovementSpeed = noteJumpMovementSpeed;
        NotesCount = notesCount;
    }
}
