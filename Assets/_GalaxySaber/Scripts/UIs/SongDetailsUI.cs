using _GalaxySaber;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongDetailsUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Button startButton;

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

    private BeatMapInfo selectedBeatMapInfo;
    private int selectedDifficultyIndex;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void Start()
    {
        EventManager.StartListening(Consts.Events.Game.Loading, HideDetails);
    }

    private void OnStartButtonClicked()
    {
        GameLoader.LoadMap(selectedBeatMapInfo, selectedDifficultyIndex);
    }

    private void HideDetails()
    {
        canvas.SetActive(false);

        difficultySets = null;
        selectedBeatMapInfo = null;
        selectedDifficultyIndex = 0;
    }

    public void DisplayDetails(BeatMapInfo beatMapInfo, string songLength, Sprite songImage)
    {
        if (!canvas.activeSelf)
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

        SelectFirstDifficulty();

        difficultyDropdown.RefreshShownValue();

        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);

        selectedBeatMapInfo = beatMapInfo;
    }

    private void SelectFirstDifficulty() => OnDifficultyChanged(0);

    private void OnDifficultyChanged(int value)
    {
        notesCountText.text = difficultySets[value].NotesCount.ToString();

        selectedDifficultyIndex = value;
    }
}