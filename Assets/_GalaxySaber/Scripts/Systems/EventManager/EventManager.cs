using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    private Dictionary<string, Action> events;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        events = new Dictionary<string, Action>();
    }

    public static void StartListening(string eventName, Action listener)
    {
        if (!instance)
        {
            Debug.LogWarning("EventManager not found");
            return;
        }

        if (instance.events.TryGetValue(eventName, out var action))
        {
            action += listener;
            instance.events[eventName] = action;
        }
        else
        {
            action += listener;
            instance.events.Add(eventName, action);
        }
    }

    public static void StopListening(string eventName, Action listener)
    {
        if (!instance){
            Debug.LogWarning("EventManager not found");
            return;
        }
        
        if(!instance.events.TryGetValue(eventName, out var action))
            return;
        
        action -= listener;
        instance.events[eventName] = action;
    }

    public static void TriggerEvent(string eventName)
    {
        if (!instance)
        {
            Debug.LogWarning("EventManager not found");
            return;
        }
        
        if(instance.events.TryGetValue(eventName, out var action))
            action?.Invoke();
    }
}
