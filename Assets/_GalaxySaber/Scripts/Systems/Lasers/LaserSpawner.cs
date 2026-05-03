using System;
using System.Collections.Generic;
using _GalaxySaber;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LasersPool))]
public class LaserSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> m_spawnPoints;
    [SerializeField] private float m_spawnInterval = 1f;

    private LasersPool _lasersPool;
    private bool _isSpawning;

    private void Awake()
    {
        _lasersPool = GetComponent<LasersPool>();
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

    public void StartSpawning()
    {
        _isSpawning = true;
        InvokeRepeating(nameof(SpawnRandomLaser), 0f, m_spawnInterval);
    }
    
    public void StopSpawning()
    {
        _isSpawning = false;
        CancelInvoke(nameof(SpawnRandomLaser));
    }

    public void SpawnRandomLaser()
    {
        var spawnPoint = GetRandomSpawnPoint();
        FireLaser(spawnPoint);
    }

    private void FireLaser(Transform spawnPoint)
    {
        _lasersPool.GetLaser(spawnPoint.position, spawnPoint.rotation);
    }
    
    private Transform GetRandomSpawnPoint()
    {
        var randomIndex = Random.Range(0, m_spawnPoints.Count);
        return m_spawnPoints[randomIndex];
    }
}
