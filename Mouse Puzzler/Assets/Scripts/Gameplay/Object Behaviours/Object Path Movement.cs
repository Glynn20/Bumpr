using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ObjectPathMovement : MonoBehaviour, IActivatable, IReset
{
    public bool IsActivatedOnStart 
    {
        get { return _isActivatedOnStart; } 
        set { _isActivatedOnStart = value; }
    }
    public bool IsActivated{get; set;}

    [Header("Behaviour")]
    public bool IsWrapping = false;
    public bool DespawnAtLastPoint = false;
    public float InitialDelay = 0f;

    [SerializeField] private bool _isActivatedOnStart = false;

    [Header("Points")]
    [SerializeField] private Transform[] _path;

    [Header("Tweening")]
    [Range(0.01f, 10.0f)] public float TimeBetweenMove = 1.0f;
    [Range(0.05f, 10.0f)] public float OscillationDuration = 1.0f;
    [SerializeField] private Ease _ease = Ease.Linear;
    
    private float _remainingInitialDelay;
    private float _remainingTimeBetweenMove = 0;    
    private int _currentPointIndex = 0;
    private Coroutine _moveCoroutine = null;
    private bool _isAtLastPoint = false;
    private Transform _transform;

    public void SetNewPath(Transform[] newPath)
    {
        if (newPath.Length > 1)
        {
            _path = newPath;
            _isAtLastPoint = false;
            
            _currentPointIndex = 0;
            _transform.position = _path[0].position;

            //_remainingTimeBetweenMove = TimeBetweenMove;
        }
    }
    public void Reset()
    {
        Deactivate();

        if (_path.Length <= 1)
        {
            _isAtLastPoint = true;
        }
        else _isAtLastPoint = false;

        _remainingInitialDelay = InitialDelay;
        //_remainingTimeBetweenMove = TimeBetweenMove;
        _currentPointIndex = 0;
        _transform.position = _path[0].position;

        if (_isActivatedOnStart) Activate();
    }
    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }
    public void Activate()
    {
        IsActivated = true;
    }

    public void Deactivate()
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
        _transform.DOKill();

        IsActivated = false;
    }
    void Awake()
    {
        _transform = transform;
        _remainingInitialDelay = InitialDelay;
        
        if (_isActivatedOnStart) Activate();
    }

    void Update()
    {
        if (!IsActivated) return;
        if (!IsWrapping && _isAtLastPoint) return;
        if (_moveCoroutine != null) return;

        if (_remainingInitialDelay > 0f)
        {
            _remainingInitialDelay -= Time.deltaTime;
            return;
        }

        _remainingTimeBetweenMove -= Time.deltaTime;
        if (_remainingTimeBetweenMove > 0) return;

        if (CanMove() && _moveCoroutine == null)
        {
            UpdateCurrentPointIndex();
            _moveCoroutine = StartCoroutine(MoveToNextPointCoroutine());
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

    private bool CanMove()
    {
        if (_currentPointIndex >= (_path.Length - 1))
        {
            if (!IsWrapping) return false;
        }
        return true;
    }

    private void UpdateCurrentPointIndex()
    {
        if (_currentPointIndex >= (_path.Length - 1) && IsWrapping)
        {
            _currentPointIndex = 0;
            _isAtLastPoint = false;
        }
        else 
        {
            ++_currentPointIndex;
            if (_currentPointIndex >= (_path.Length - 1)) 
            {
                _isAtLastPoint = true;
            }
        }
    }

    private IEnumerator MoveToNextPointCoroutine()
    {
        yield return _transform.DOMove(_path[_currentPointIndex].position, OscillationDuration).SetEase(_ease).WaitForCompletion();
        _moveCoroutine = null;

        if (_isAtLastPoint && DespawnAtLastPoint) Destroy(gameObject);

        _remainingTimeBetweenMove = TimeBetweenMove;
    }
}

