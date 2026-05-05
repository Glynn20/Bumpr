using System;
using System.Data.Common;
using System.Net.Sockets;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Bomb : MonoBehaviour, IReset
{
    [Header("Parameters")]
    [SerializeField] private float _timeBeforeExplosion = 3.0f;
    [SerializeField] private float _explosionRadius = 3.0f;
    [SerializeField] private Transform _spawnLocation;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _remainingTime;
    private bool _canAttach = true;
    private bool _timerEnabled = false;
    private Transform _transform;
    private ICanAttachBomb _currentSocket;

    public void Reset()
    {
        _currentSocket = null;
        _canAttach = true;
        _transform.parent = null;
        
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

        SetSpawn();

        UpdateTimerText();
    }

    void Update()
    {
        if (!_timerEnabled) return;

        _remainingTime -= Time.deltaTime;
        if (_timerText != null)
        {
            UpdateTimerText();
        }
        
        if (_remainingTime > 0f) return;

        Explode();
    }

    private void SetSpawn()
    {
        if (_spawnLocation == null) _transform.position = _spawnLocation.position;
        else                        _transform.position = _spawnLocation.position;
    }
    void OnTriggerEnter2D(Collider2D other)
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
        }
        else
        {
            StartTimer();
        }
    }

    void UpdateTimerText()
    {
        _timerText.text = Math.Round(_remainingTime, 1).ToString();
    }
    void StartTimer()
    {
        _timerEnabled = true;
    }

    void StopTimer()
    {
        _timerEnabled = false;
    }
    void ResetTimer()
    {
        _remainingTime = _timeBeforeExplosion;
    }
} 
