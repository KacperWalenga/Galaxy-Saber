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
        EventManager.StartListening(Consts.Events.UI.LoadPlayMusicMenu, LoadMusicMenu);
        EventManager.StartListening(Consts.Events.UI.DisplayPlayMusicMenu, DisplayMusicMenu);
        EventManager.StartListening(Consts.Events.Game.Loading, HideMusicMenu);
    }

    private void HideMusicMenu()
    {
        canvas.SetActive(false);
    }

    private void DisplayMusicMenu()
    {
        canvas.SetActive(true);
    }

    private void LoadMusicMenu()
    {
        BeatMapLoader.LoadBeatMapsInfos();
        
        foreach (var beatMapInfo in BeatMapLoader.BeatMapsInfo)
        {
            var songImage = ImageLoader.LoadSpriteFromFile(beatMapInfo.CoverImageFilename) ?? defaultSongImage;
            var duration = SongMetadataReader.GetFormattedSongDuration(beatMapInfo.SongFileName);
            
            var songItem = Instantiate(songItemPrefab, contentParent);
            
            void OnClick() => OnSongClick(beatMapInfo, duration, songImage);

            songItem.Setup(beatMapInfo, duration, songImage, OnClick);
        }

        DisplayMusicMenu();
    }

    private void OnSongClick(BeatMapInfo beatMapInfo, string songLength, Sprite songImage)
    {
        songDetails.DisplayDetails(beatMapInfo, songLength, songImage);
    }
}
