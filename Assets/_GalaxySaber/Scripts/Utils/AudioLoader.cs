using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class AudioLoader
{
    public static async Task<AudioClip> LoadAudioClip(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return null;
        }

        var extension = Path.GetExtension(filePath).ToLower();

        var audioType = extension switch
        {
            ".mp3" => AudioType.MPEG,
            ".wav" => AudioType.WAV,
            ".ogg" => AudioType.OGGVORBIS,
            ".egg" => AudioType.OGGVORBIS,
            ".aiff" => AudioType.AIFF,
            _ => AudioType.UNKNOWN
        };

        if (audioType == AudioType.UNKNOWN)
        {
            Debug.LogError($"Unsupported audio format: {extension}");
            return null;
        }

        var url = "file://" + Path.GetFullPath(filePath).Replace("\\", "/");

        var request = UnityWebRequestMultimedia.GetAudioClip(url, audioType);

        var operation = request.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Audio load failed: {request.error}");
            return null;
        }

        return DownloadHandlerAudioClip.GetContent(request);
    }
}