using System;
using System.Data.Common;
using System.Net.Sockets;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool _canAttach = true;
    private bool _timerEnabled = false;
    private Transform _transform;
    private ICanAttachBomb _currentSocket;

    [Header("Parameters")]
    [SerializeField] private float _timeBeforeExplosion = 3.0f;
    [SerializeField] private float _explosionRadius = 3.0f;

    [Header("UI")]

    [SerializeField] private TextMeshProUGUI _timerText;
    void Awake()
    {
        _transform = transform;
        UpdateTimerText();
    }

    void Update()
    {
        if (!_timerEnabled) return;

        _timeBeforeExplosion -= Time.deltaTime;
        if (_timerText != null)
        {
            UpdateTimerText();
        }
        
        if (_timeBeforeExplosion > 0f) return;

        Explode();
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
        _timerText.text = Math.Round(_timeBeforeExplosion, 1).ToString();
    }
    void StartTimer()
    {
        _timerEnabled = true;
    }

    void StopTimer()
    {
        _timerEnabled = false;
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
} 
