using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

public static class BeatMapNoteCounter
{
    public static int CountNotes(string beatmapPath)
    {
        if (!File.Exists(beatmapPath))
            return 0;

        var json = File.ReadAllText(beatmapPath);
        var jObject = JObject.Parse(json);

        if (jObject["colorNotes"] is { HasValues: true })
            return jObject["colorNotes"].Count();
        
        if (jObject["_notes"] is { HasValues: true })
            return jObject["_notes"].Count();

        return 0;
    }
}