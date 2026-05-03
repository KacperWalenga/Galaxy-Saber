using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SongItemUI : MonoBehaviour
{
    [SerializeField] private Button button;
    
    [Header("Song information")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text author;
    [SerializeField] private TMP_Text length;
    [SerializeField] private Image image;

    private RectTransform rectTransform;
    
    private void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void Setup(BeatMapInfo beatMapInfo, string songLength, Sprite songImage, UnityAction onClick)
    {
        
        title.text = beatMapInfo.SongName;
        author.text = beatMapInfo.SongAuthor;
        length.text = songLength;
        image.sprite = songImage;
        
        button.onClick.AddListener(onClick);
    }

    private void OnButtonClick()
    {
        
    }
}
