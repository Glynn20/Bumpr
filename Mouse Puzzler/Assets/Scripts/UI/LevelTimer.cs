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
        int minutes = (int)_levelTime / 60;

        
        return $"{minutes:D2}:{seconds:D2}";
    }
}
