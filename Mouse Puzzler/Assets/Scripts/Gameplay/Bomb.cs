using System;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour, IReset
{
    [Header("Parameters")]
    [SerializeField] private float _timeBeforeExplosion = 3.0f;
    [SerializeField] private float _explosionRadius = 3.0f;
    [SerializeField] private Transform _spawnLocation;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Sound")]
    [SerializeField] private SoundData _pickupSoundData;
    [SerializeField] private SoundData _FuseLowSoundData;
    [SerializeField] private SoundData _FuseMediumSoundData;
    [SerializeField] private SoundData _FuseHighSoundData;
    private SoundData _currentSound;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;
    [SerializeField] private GameEvent OnStopSound;
    [Space]
    [SerializeField] private float mediumFuseThreshold = 0.67f;
    [SerializeField] private float highFuseThreshold = 0.34f;

    private float RemainingFusePercentage => (float)_remainingTime / _timeBeforeExplosion;
    private float _remainingTime;
    private bool _canAttach = true;
    private bool _timerEnabled = false;
    private Transform _transform;
    private ICanAttachBomb _currentSocket;
    private FuseState _currentFuseState;
    private enum FuseState
    {   
        Low = 0,
        Medium = 1,
        High = 2 
    }


    public void Reset()
    {
        _currentSocket = null;
        _canAttach = true;
        _transform.parent = null;
        
        StopSound(_currentSound);
        _currentFuseState = FuseState.Low;
        SetSpawn();
        
        StopTimer();
        ResetTimer();
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
    public void Explode()
    {
        StopTimer();
        Collider2D[] hits = Physics2D.OverlapCircleAll(_transform.position, _explosionRadius);
        foreach( Collider2D hit in hits)
        {
            hit.GetComponent<IHealth>()?.TakeDamage();
        }
    }

    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }

    void Awake()
    {
        _transform = transform;
        _remainingTime = _timeBeforeExplosion;
        _currentSound = null;
        
        SetSpawn();
        UpdateTimerText();

    }

    void Update()
    {
        if (!_timerEnabled) return;

        _remainingTime -= Time.deltaTime;
        
        UpdateTimerText();

        FuseState targetState = CheckFuseState();
        if (_currentFuseState != targetState)
        {
            ChangeFuseState(targetState);
        }        
        
        if (_remainingTime > 0f) return;

        Explode();
    }
    private void PlaySound(SoundData soundToPlay)
    {
        OnPlaySound?.Raise(this, soundToPlay);
        _currentSound = soundToPlay;
    }
    private void StopSound(SoundData soundToStop)
    {
        if (soundToStop == null) return;

        OnStopSound?.Raise(this, soundToStop);
        _currentSound = null;
    }
    private void SetSpawn()
    {
        if (_spawnLocation == null) _transform.position = Vector2.zero;
        else                        _transform.position = _spawnLocation.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        ICanAttachBomb socket = other.gameObject.GetComponent<ICanAttachBomb>();
        
        if (socket == null || _canAttach == false || socket == _currentSocket) return;
        
        _transform.SetParent(socket.BombAttachPoint);
        _transform.SetPositionAndRotation(socket.BombAttachPoint.position, socket.BombAttachPoint.rotation);
        _currentSocket = socket;

        if (socket.IsPermanentAttachPoint) 
        {
            _canAttach = false;
            StopTimer();
            StopSound(_currentSound);
        }
        else
        {
            StartTimer();
            PlaySound(_FuseLowSoundData);
        }
    }

    private void UpdateTimerText()
    {
        if (_timerText == null) return;
        _timerText.text = Math.Round(_remainingTime, 1).ToString();
    }

    private FuseState CheckFuseState()
    {
        FuseState targetState = FuseState.Low;
        if (RemainingFusePercentage < highFuseThreshold)
        {
            targetState = FuseState.High;
        }
        else if (RemainingFusePercentage < mediumFuseThreshold)
        {
            targetState = FuseState.Medium;
        }
        return targetState;
    }
    private void ChangeFuseState(FuseState newState)
    {
        switch (newState)
        {
            case FuseState.Medium:
                _currentFuseState = newState;
                StopSound(_FuseLowSoundData);
                PlaySound(_FuseMediumSoundData);
                return;
            case FuseState.High:
                _currentFuseState = newState;
                StopSound(_FuseMediumSoundData);
                PlaySound(_FuseHighSoundData);
                return;
        }
    }
    private void StartTimer()
    {
        _timerEnabled = true;
    }

    private void StopTimer()
    {
        _timerEnabled = false;
    }
    private void ResetTimer()
    {
        _remainingTime = _timeBeforeExplosion;
    }
} 
