using System.Collections;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    [SerializeField] private bool activeOnStart = true;

    [Header("References")]
    [SerializeField] private Transform blade;
    [SerializeField] private Light bladeLight;

    [Header("Audio")]
    [SerializeField] private AudioSource humSource;

    [Header("Animation")]
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private float maxBladeLength = 1.0f;
    [SerializeField] private float maxLightIntensity = 1.0f;

    private bool isActive;
    private Coroutine animRoutine;

    private void Start()
    {
        isActive = activeOnStart;
        SetActive(isActive);

        if (!humSource)
            return;
        
        if (isActive) 
            humSource.Play();
        else 
            humSource.Stop();
    }

    public void EnableLightsaber()
    {
        if (isActive) 
            return;
        
        SetActive(true);
    }

    public void DisableLightsaber()
    {
        if (!isActive) 
            return;
        
        SetActive(false);
    }
    
    public void ToggleLightsaber()
    {
        SetActive(!isActive);
    }

    private void SetActive(bool value)
    {
        isActive = value;

        if (humSource)
        {
            if (isActive) 
                humSource.Play();
            else 
                humSource.Stop();
        }

        if (animRoutine != null) StopCoroutine(animRoutine);
        animRoutine = StartCoroutine(Animate(isActive));
    }

    private IEnumerator Animate(bool turnOn)
    {
        if (!blade && !bladeLight)
            yield break;

        var startTime = Time.time;

        var startScale = blade ? blade.localScale : Vector3.one;
        var endScale = startScale;

        if (blade)
            endScale = new Vector3(startScale.x, turnOn ? maxBladeLength : 0f, startScale.z);

        var startIntensity = bladeLight ? bladeLight.intensity : 0f;
        var endIntensity = turnOn ? maxLightIntensity : 0f;

        var t = 0f;
        while (t < 1f)
        {
            t = duration <= 0f ? 1f : (Time.time - startTime) / duration;
            t = Mathf.Clamp01(t);

            var s = t * t * (3f - 2f * t);

            if (blade)
                blade.localScale = Vector3.LerpUnclamped(startScale, endScale, s);

            if (bladeLight)
                bladeLight.intensity = Mathf.LerpUnclamped(startIntensity, endIntensity, s);

            yield return null;
        }

        if (blade) blade.localScale = endScale;
        if (bladeLight) bladeLight.intensity = endIntensity;

        animRoutine = null;
    }
}
