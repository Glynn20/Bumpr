using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimedDoor : MonoBehaviour, IActivatable, IReset
{
    public bool IsActivatedOnStart {get; set;}
    public bool IsActivated{get; set;}

    [Header("Behaviour")]
    [SerializeField] private float _timeBeforeOpen = 2.0f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timerText;

    private SpriteRenderer _renderer;
    private Collider2D _collider;
    
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
    public void Reset()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
        _timerText.enabled = true;
        Deactivate();
        ResetTime();
        UpdateTimerText();
    }
    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }
    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }
    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<Collider2D>();
        ResetTime();
        UpdateTimerText();
    }

    private void Update()
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
            _timerText.enabled = false;
            Deactivate();
        }
    }
    private void Open()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
    }
    private void ResetTime()
    {
        _remainingTime = _timeBeforeOpen;
    }
    private void UpdateTimerText()
    {
        _timerText.text = Math.Round(_remainingTime, 1).ToString();
    }
}
