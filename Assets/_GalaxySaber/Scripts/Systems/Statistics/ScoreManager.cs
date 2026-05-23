using System;
using _GalaxySaber;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int baseScorePerHit = 100;
    [filed: SerializeField] public int MaxRating { get; private set; } = 10;

    private float scoreMultiplier = 1f;

    public int Score { get; private set; }
    public UnityEvent<int> OnScoreChange = new();

    private int pointsPerHit;
    private int currentNotesCount => MissedNotesCount + HittedNotesCount;
    public int MissedNotesCount { get; private set; }
    public int HittedNotesCount { get; private set; }
    
    public static ScoreManager Instance { get; private set; }
    
    public int Rating
    {
        get
        {
            if (currentNotesCount <= 0 || pointsPerHit <= 0)
                return 0;

            var currentMaxScore = currentNotesCount * pointsPerHit;

            return Mathf.Clamp(
                Mathf.RoundToInt((float)Score / currentMaxScore * MaxRating),
                0,
                MaxRating
            );
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventManager.StartListening(Consts.Events.Game.Started, GameStarted);
        EventManager.StartListening(Consts.Events.Laser.Hit, LaserHit);
        EventManager.StartListening(Consts.Events.Laser.Missed, LaserMissed);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Consts.Events.Game.Started, GameStarted);
        EventManager.StopListening(Consts.Events.Laser.Hit, LaserHit);
        EventManager.StopListening(Consts.Events.Laser.Missed, LaserMissed);

        Instance = null;
    }

    private void GameStarted()
    {
        Score = 0;
        HittedNotesCount = 0;
        MissedNotesCount = 0;

        var difficultyIndex = GameLoader.currentDifficulty;

        scoreMultiplier = Mathf.Pow(1.2f, difficultyIndex);
        pointsPerHit = Mathf.RoundToInt(baseScorePerHit * scoreMultiplier);

        OnScoreChange.Invoke(Score);
    }

    private void LaserHit()
    {
        AddScore(pointsPerHit);
        HittedNotesCount += 1;
    }

    private void LaserMissed()
    {
        MissedNotesCount += 1;
    }

    private void AddScore(int score)
    {
        Score += score;

        OnScoreChange.Invoke(Score);
    }
}