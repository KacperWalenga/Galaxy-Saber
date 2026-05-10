using Newtonsoft.Json.Linq;

public static class BeatMapParser
{
    public static BeatMapInfo ParseBeatMapInfo(string json, string path)
    {
        JObject jObject;

        try
        {
            jObject = JObject.Parse(json);
        }
        catch (System.Exception e)
        {
            throw new InvalidBeatMapException("Invalid JSON in info.dat", e);
        }

        var version = GetVersion(jObject);

        if (string.IsNullOrWhiteSpace(version))
            throw new InvalidBeatMapException("Missing version in info.dat");

        if (version.StartsWith("2"))
        {
            var dto = jObject.ToObject<BeatMapInfoV2Dto>();
            if (dto == null)
                throw new InvalidBeatMapException("Failed to deserialize BeatMap v2");

            return dto.ToModel(path);
        }

        if (version.StartsWith("4"))
        {
            var dto = jObject.ToObject<BeatMapInfoV4Dto>();
            if (dto == null)
                throw new InvalidBeatMapException("Failed to deserialize BeatMap v4");

            return dto.ToModel(path);
        }

        throw new UnsupportedBeatMapVersionException(version);
    }

    public static BeatMap ParseBeatMap(string json)
    {
        JObject jObject;

        try
        {
            jObject = JObject.Parse(json);
        }
        catch (System.Exception e)
        {
            throw new InvalidBeatMapException("Invalid JSON in info.dat", e);
        }

        var version = GetVersion(jObject);

        if (string.IsNullOrWhiteSpace(version))
            throw new InvalidBeatMapException("Missing version in info.dat");
        

        if (version.StartsWith("4"))
        {
            var dto = jObject.ToObject<BeatMapV4Dto>();
            if (dto == null)
                throw new InvalidBeatMapException("Failed to deserialize BeatMap v4");

            return dto.ToModel();
        }

        throw new UnsupportedBeatMapVersionException(version);
    }

    private static string GetVersion(JObject jObject)
    {
        return jObject["version"]?.ToString()
               ?? jObject["_version"]?.ToString();
    }
}