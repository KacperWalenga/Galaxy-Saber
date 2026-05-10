using System;
using _GalaxySaber;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Button startButton;
    
    private void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        canvas.SetActive(false);
        
        EventManager.TriggerEvent(Consts.Events.RefreshSongs);
        EventManager.TriggerEvent(Consts.Events.UI.LoadPlayMusicMenu);
    }
}
