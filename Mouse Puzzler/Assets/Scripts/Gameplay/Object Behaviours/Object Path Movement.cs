using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ObjectPathMovement : MonoBehaviour, IActivatable
{
    public bool IsActivatedOnStart 
    {
        get { return _isActivatedOnStart; } 
        set { _isActivatedOnStart = value; }
    }
    public bool IsActivated{get; set;}


    [SerializeField] private bool _isActivatedOnStart;


    [Header("Points")]
    [SerializeField] private Transform[] _points;
    private Transform _transform;

    [Header("Tweening")]
    [SerializeField] private Ease _ease = Ease.Linear;
    [Range(0.05f, 10.0f)] [SerializeField] private float _timeBetweenMove;
    [Range(0.05f, 10.0f)] [SerializeField] private float _oscillationDuration;
    
    [Header("Behaviour")]
    [SerializeField] private bool _isWrapping = false;
    private float _remainingTimeBetweenMove;    
    private int _currentPointIndex = 0;
    private Coroutine _moveCoroutine = null;
    private bool _isAtLastPoint = false;

    void Awake()
    {
        _transform = transform;
        if (_points.Length == 0)
        {
            Debug.LogError("Oscillation Points Array is empty. Set in inspector", this);
            _isAtLastPoint = true;
        }
        _transform.position = _points[0].position;
        _remainingTimeBetweenMove = _timeBetweenMove;
    }

    void Update()
    {
        if (!IsActivated) return;
        if (!_isWrapping && _isAtLastPoint) return;
        if (_moveCoroutine != null) return;

        _remainingTimeBetweenMove -= Time.deltaTime;

        if (_remainingTimeBetweenMove > 0) return;

        if (CanMove() && _moveCoroutine == null)
        {
            UpdateCurrentPointIndex();
            _moveCoroutine = StartCoroutine(MoveCoroutine());
        }
    }

    public void Activate()
    {
        IsActivated = true;
    }

    public void Deactivate()
    {
        IsActivated = false;
    }

    private bool CanMove()
    {
        if (_currentPointIndex >= (_points.Length - 1))
        {
            if (!_isWrapping) return false;
        }
        return true;
    }

    private void UpdateCurrentPointIndex()
    {
        if (_currentPointIndex >= (_points.Length - 1) && _isWrapping)
        {
            _currentPointIndex = 0;
        }
        else 
        {
            ++_currentPointIndex;
            if (_currentPointIndex >= (_points.Length - 1)) 
            {
                _isAtLastPoint = true;
            }
        }
    }

    private IEnumerator MoveCoroutine()
    {
        yield return _transform.DOMove(_points[_currentPointIndex].position, _oscillationDuration).SetEase(_ease).WaitForCompletion();
        _moveCoroutine = null;
        _remainingTimeBetweenMove = _timeBetweenMove;
    }
}

