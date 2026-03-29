using System;

public class BeatMapLoadException : Exception
{
    public BeatMapLoadException(string message) : base(message) { }
    public BeatMapLoadException(string message, Exception innerException) : base(message, innerException) { }
}

public class InvalidBeatMapException : BeatMapLoadException
{
    public InvalidBeatMapException(string message) : base(message) { }
    public InvalidBeatMapException(string message, Exception innerException) : base(message, innerException) { }
}

public class UnsupportedBeatMapVersionException : BeatMapLoadException
{
    public UnsupportedBeatMapVersionException(string version)
        : base($"Unsupported BeatMap version: {version}") { }
}