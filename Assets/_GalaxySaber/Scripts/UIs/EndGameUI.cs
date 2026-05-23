using _GalaxySaber;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Button continueButton;

    [Header("Summary")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hittedNotesText;
    [SerializeField] private TMP_Text missedNotesText;
    [SerializeField] private TMP_Text ratingText;

    private void Awake()
    {
        continueButton.onClick.AddListener(ContinueButtonClicked);
    }

    private void ContinueButtonClicked()
    {
        canvas.SetActive(false);
        EventManager.TriggerEvent(Consts.Events.UI.DisplayPlayMusicMenu);
    }

    private void Start()
    {
        EventManager.StartListening(Consts.Events.Game.Ended, GameEnded);
    }

    private void GameEnded()
    {
        var scoreManager = ScoreManager.Instance;
        var score = scoreManager.Score;
        var hittedNotes = scoreManager.HittedNotesCount;
        var missedNotes = scoreManager.MissedNotesCount;
        var rating = scoreManager.Rating;
        var maxRating = scoreManager.MaxRating;
        
        scoreText.text = $"Score: {score}";
        hittedNotesText.text = $"Hitted Notes: {hittedNotes}";
        missedNotesText.text = $"Missed Notes: {missedNotes}";
        ratingText.text = $"Rating: {rating} / {maxRating}";
        
        canvas.SetActive(true);
    }
}