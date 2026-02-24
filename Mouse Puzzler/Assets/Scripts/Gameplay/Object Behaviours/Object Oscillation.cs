using System.Collections;
using UnityEngine;
using DG.Tweening;
public class ObjectOscillation : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform[] _points;
    private Transform _transform;

    [Header("Tweening")]
    [SerializeField] private Ease _ease = Ease.Linear;
    [Range(0.05f, 10.0f)] [SerializeField] private float _timeBetweenOscillation;
    [Range(0.05f, 10.0f)] [SerializeField] private float _oscillationDuration;
    
    private float _remainingTime;
    private int _currentPointIndex = 0;
    private Coroutine _oscillationCoroutine = null;

    void Awake()
    {
        _transform = transform;
        if (_points.Length == 0)
        {
            Debug.LogError("Oscillation Points Array is empty. Set in inspector", this);
        }
        _transform.position = _points[0].position;
        _remainingTime = _timeBetweenOscillation;
    }

    void Update()
    {
        if (_oscillationCoroutine != null) return;
        _remainingTime -= Time.deltaTime;

        if (_remainingTime > 0) return;

        if (_oscillationCoroutine == null)
        {
            _oscillationCoroutine = StartCoroutine(OscillateCoroutine());
        }
    }

    private int GetNextPointIndex()
    {
        int nextIndex;
        if (_currentPointIndex == (_points.Length - 1))
        {
            nextIndex = 0; 
        }
        else
        {
            nextIndex = _currentPointIndex + 1;
        }
        return nextIndex;
    }
    private void UpdateIndex()
    {
        if (_currentPointIndex == (_points.Length - 1))
        {
            _currentPointIndex = 0;
        }
        else 
        {
            ++_currentPointIndex;
        }
    }

    private IEnumerator OscillateCoroutine()
    {
        yield return _transform.DOMove(_points[GetNextPointIndex()].position, _oscillationDuration).SetEase(_ease).WaitForCompletion();
        UpdateIndex();
        _oscillationCoroutine = null;
        _remainingTime = _timeBetweenOscillation;
    }
}
