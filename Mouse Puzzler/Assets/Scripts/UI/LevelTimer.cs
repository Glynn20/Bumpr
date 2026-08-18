using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clock;
    private float _levelTime = 0f;
    private bool _isStopped = false;

    public void OnLevelBegin()
    {
        ResetLevelTimer();
    }

    public void OnLevelComplete()
    {
        StopTimer();
    }

    private void FixedUpdate()
    {
        if (_isStopped) return;

        _levelTime += Time.deltaTime;
        _clock.text = GetClockTime();
    }

    private void ResetLevelTimer()
    {
        _levelTime = 0f;
    }

    private void StopTimer()
    {
        _isStopped = true;
    }

    public string GetClockTime()
    {
        if (_clock == null) return "Error";

        int seconds = (int) _levelTime % 60;
        string secondsText = seconds >= 10 ? $"{seconds}" : $"0{seconds}";
        
        if (_levelTime < 60)
        {
            return $"00:{secondsText}";
        }

        int minutes = (int) Math.Round((_levelTime / 60), 1);
        string minutesText = minutes >= 10 ? $"{minutes}" : $"0{minutes}";
        
        return $"{minutesText}:{secondsText}";
    }
}
