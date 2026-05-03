using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongDetailsUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    
    [Header("Song information")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text authorText;
    [SerializeField] private TMP_Text lengthText;
    [SerializeField] private TMP_Text notesCountText;
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private Image image;

    public DifficultySet SelectedDifficulty => difficultySets[currentDifficulty];
    
    private int currentDifficulty => difficultyDropdown.value;
    private DifficultySet[] difficultySets;
    
    public void DisplayDetails(BeatMapInfo beatMapInfo, string songLength, Sprite songImage)
    {
        if(!canvas.activeSelf)
            canvas.SetActive(true);
        
        titleText.text = beatMapInfo.SongName;
        authorText.text = beatMapInfo.SongAuthor;
        lengthText.text = songLength;
        image.sprite = songImage;
        difficultySets = beatMapInfo.DifficultySets;
        
        difficultyDropdown.Hide();
        difficultyDropdown.options.Clear();
        
        foreach (var difficultySet in difficultySets)
        {
            var optionData = new TMP_Dropdown.OptionData(difficultySet.Difficulty);
            difficultyDropdown.options.Add(optionData);
        }
        
        OnDifficultyChanged(0);
        
        difficultyDropdown.RefreshShownValue();
        
        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
    }

    private void OnDifficultyChanged(int value)
    {
        notesCountText.text = difficultySets[value].NotesCount.ToString();
    }
}
