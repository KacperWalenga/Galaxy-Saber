using System.Collections.Generic;
using _GalaxySaber;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LasersPool))]
public class LaserSpawner : MonoBehaviour
{
    [SerializeField] private List<LaserSpawnPoints> spawnPoints;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform hitPoint;

    public float LasersSpeed = 5f;

    private LasersPool lasersPool;
    private bool isSpawning;
    private List<BeatData> beats;
    private int nextBeatIndex;

    private void Awake()
    {
        lasersPool = GetComponent<LasersPool>();
    }

    private void Start()
    {
        EventManager.StartListening(Consts.Events.Game.Started, StartSpawning);
        EventManager.StartListening(Consts.Events.Game.Ended, StopSpawning);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Consts.Events.Game.Started, StartSpawning);
        EventManager.StopListening(Consts.Events.Game.Ended, StopSpawning);
    }

    public void Init(List<BeatData> beatDataList, float speed)
    {
        beats = beatDataList;
        nextBeatIndex = 0;
        lasersPool.SetSpeed(speed);
    }

    private void Update()
    {
        if (!isSpawning || beats == null || nextBeatIndex >= beats.Count)
            return;

        var currentTime = audioSource.time;

        while (nextBeatIndex < beats.Count)
        {
            var beat = beats[nextBeatIndex];

            var spawnPoint = GetSpawnPoint(beat.LineIndex, beat.LineLayer);

            if (spawnPoint == null)
            {
                nextBeatIndex++;
                continue;
            }

            var travelTime = CalculateTravelTime(spawnPoint);
            var spawnTime = Mathf.Max(0f, beat.Time - travelTime);

            if (currentTime < spawnTime)
                break;

            FireLaser(spawnPoint);
            nextBeatIndex++;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void SpawnRandomLaser()
    {
        var spawnPoint = GetRandomSpawnPoint();

        if (spawnPoint != null)
            FireLaser(spawnPoint);
    }

    private float CalculateTravelTime(Transform spawnPoint)
    {
        var distance = Vector3.Distance(spawnPoint.position, hitPoint.position);
        return distance / lasersPool.LaserSpeed;
    }

    private void FireLaser(Transform spawnPoint)
    {
        lasersPool.GetLaser(spawnPoint.position, spawnPoint.rotation);
    }

    private Transform GetSpawnPoint(int lineIndex, int lineLayer)
    {
        foreach (var point in spawnPoints)
        {
            if (point.LineIndex == lineIndex &&
                point.LineLayer == lineLayer)
            {
                return point.SpawnPoint;
            }
        }

        Debug.LogWarning(
            $"No spawn point for lineIndex {lineIndex}, lineLayer {lineLayer}"
        );

        return null;
    }

    private Transform GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
            return null;

        var randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex].SpawnPoint;
    }
}
