using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongDetailsUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    
    [Header("Song information")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text author;
    [SerializeField] private TMP_Text length;
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private Image image;

    public DifficultySet SelectedDifficulty => difficultySets[currentDifficulty];
    
    private int currentDifficulty => difficultyDropdown.value;
    private DifficultySet[] difficultySets;
    
    public void DisplayDetails(BeatMapInfo beatMapInfo, string songLength, Sprite songImage)
    {
        if(!canvas.activeSelf)
            canvas.SetActive(true);
        
        title.text = beatMapInfo.SongName;
        author.text = beatMapInfo.SongAuthor;
        length.text = songLength;
        image.sprite = songImage;
        difficultySets = beatMapInfo.DifficultySets;
        
        difficultyDropdown.Hide();
        difficultyDropdown.options.Clear();
        
        foreach (var difficultySet in difficultySets)
        {
            var optionData = new TMP_Dropdown.OptionData(difficultySet.Difficulty);
            difficultyDropdown.options.Add(optionData);
        }
        
        difficultyDropdown.RefreshShownValue();
    } 
}
