using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimedDoor : MonoBehaviour, IActivatable
{
    public bool IsActivatedOnStart {get; set;}
    public bool IsActivated{get; set;}

    [Header("Behaviour")]
    [SerializeField] private float _timeBeforeOpen = 2.0f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timerText;
    
    private bool _timerEnabled = false;
    private float _remainingTime;

    public void Activate()
    {
        IsActivated = true;
        _timerEnabled = true;
    }
    public void Deactivate()
    {
        IsActivated = false;
        _timerEnabled = false;
    }
    void Awake()
    {
        _remainingTime = _timeBeforeOpen;
        UpdateTimerText();
    }

    void Update()
    {
        if (!IsActivated) return;
        if (!_timerEnabled) return;

        _remainingTime -= Time.deltaTime;
        if (_timerText != null)
        {
            UpdateTimerText();
        }

        if (_remainingTime <= 0)
        {
            Open();
        }
    }
    private void Open()
    {
        gameObject.SetActive(false);
    }
    private void UpdateTimerText()
    {
        _timerText.text = Math.Round(_remainingTime, 1).ToString();
    }
}
