using _GalaxySaber;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Button continueButton;

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
        
        canvas.SetActive(true);
    }
}