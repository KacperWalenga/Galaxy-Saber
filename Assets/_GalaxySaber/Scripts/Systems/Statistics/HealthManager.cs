using System;
using _GalaxySaber;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float defaultDamage = 10f;
    [SerializeField] private float defaultHeal = 5f;
    
    private float health;
    private float damageMultiplier = 1f;
    private float healMultiplier = 1f;
    
    public UnityEvent<float> OnHealthChange = new();
    public float MaxHealth => maxHealth;

    public static HealthManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventManager.StartListening(Consts.Events.Game.Started, GameStarted);
        EventManager.StartListening(Consts.Events.Laser.Missed, LaserMissed);
        EventManager.StartListening(Consts.Events.Laser.Hit, LaserHit);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Consts.Events.Game.Started, GameStarted);
        EventManager.StopListening(Consts.Events.Laser.Missed, LaserMissed);
        EventManager.StopListening(Consts.Events.Laser.Hit, LaserHit);
        
        Instance = null;
    }

    private void LaserHit()
    {
        AddHealth(defaultHeal * healMultiplier);
    }

    private void LaserMissed()
    {
        RemoveHealth(defaultDamage * damageMultiplier);
    }

    private void GameStarted()
    {
        health = maxHealth;

        var difficultyIndex = GameLoader.currentDifficulty;
        damageMultiplier = Mathf.Pow(1.35f, difficultyIndex);
        healMultiplier = Mathf.Pow(0.85f, difficultyIndex);
        
        OnHealthChange.Invoke(health);

    }

    private void AddHealth(float value)
    {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);
        
        OnHealthChange.Invoke(health);
    }

    private void RemoveHealth(float value)
    {
        health -= value;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0)
        {
            EventManager.TriggerEvent(Consts.Events.Game.Lost);
        }
        
        OnHealthChange.Invoke(health);
    }
}