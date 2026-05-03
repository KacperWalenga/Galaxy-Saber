using System.IO;
using UnityEngine;

public class ImageLoader : MonoBehaviour
{
    public static Sprite LoadSpriteFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            return null;
        }

        var fileData = File.ReadAllBytes(path);

        var texture = new Texture2D(2, 2);

        if (!texture.LoadImage(fileData))
        {
            Debug.LogError("Failed to load image: " + path);
            return null;
        }

        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
    }
}
