using System;
using _GalaxySaber;
using TMPro;
using UnityEngine;

public class GameScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text ratingText;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChange.AddListener(ScoreChanged);
        EventManager.StartListening(Consts.Events.Game.Started, GameStarted);
        EventManager.StartListening(Consts.Events.Game.Ended, GameEnded);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Consts.Events.Game.Started, GameStarted);
        EventManager.StopListening(Consts.Events.Game.Ended, GameEnded);
    }

    private void GameEnded()
    {
        canvas.SetActive(false);
        scoreText.text = "0";
        ratingText.text = "10/10";
    }

    private void GameStarted()
    {
        canvas.SetActive(true);
    }

    private void ScoreChanged(int score)
    {
        var rating = ScoreManager.Instance.Rating;
        var maxRating = ScoreManager.Instance.MaxRating;
        
        scoreText.text = $"{score}";
        ratingText.text = $"{rating}/{maxRating}";
    }
}