using System;
using _GalaxySaber;
using UnityEngine;
using UnityEngine.Events;

public class SelectSongUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private SongItemUI songItemPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private SongDetailsUI songDetails;
    [SerializeField] private Sprite defaultSongImage;

    private void Start()
    {
        EventManager.StartListening(Consts.Events.UI.DisplayMusicMenu, OnDisplayMusicMenu);
    }

    private void OnDisplayMusicMenu()
    {
        BeatMapLoader.LoadBeatMaps();
        
        foreach (var beatMapInfo in BeatMapLoader.BeatMaps)
        {
            var songImage = ImageLoader.LoadSpriteFromFile(beatMapInfo.CoverImageFilename) ?? defaultSongImage;
            var duration = SongMetadataReader.GetFormattedSongDuration(beatMapInfo.SongFileName);
            
            var songItem = Instantiate(songItemPrefab, contentParent);
            
            void OnClick() => OnSongClick(beatMapInfo, duration, songImage);

            songItem.Setup(beatMapInfo, duration, songImage, OnClick);
        }
        
        canvas.SetActive(true);
    }

    private void OnSongClick(BeatMapInfo beatMapInfo, string songLength, Sprite songImage)
    {
        songDetails.DisplayDetails(beatMapInfo, songLength, songImage);
    }
}
