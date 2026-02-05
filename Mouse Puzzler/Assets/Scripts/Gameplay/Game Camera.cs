using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameCamera : MonoBehaviour, IPannableCamera
{
    private Coroutine _snapCoroutine;
    private Transform _transform;

    [Header("Snap Parameters")]
    [SerializeField] private float _snapTime = 1;
    private float _minSnapTime = 0.1f;

    [Header("Events")]
    [SerializeField] private GameEvent _setPlayerInputEnabled;
    
    void Awake()
    {
        _transform = transform;
    }

        IEnumerator SnapToPointCoroutine(Transform point)
    {
        _setPlayerInputEnabled?.Raise(this, false);

        if (_snapTime < _minSnapTime) { _snapTime = _minSnapTime;}
        
        float t = 0;  
        Vector3 startPos = _transform.position;
        Vector3 endPoint = new Vector3(point.position.x, point.position.y, startPos.z);

        while (t < _snapTime)
        {
            _transform.position = Vector3.Lerp(startPos, endPoint, t / _snapTime);

            t += Time.deltaTime;
            yield return null;
        }

        // Snap directly to endpoint to avoid any margin of error
        _transform.position = endPoint;

        _setPlayerInputEnabled?.Raise(this, true);
        _snapCoroutine = null;
        yield return null;
    }

    public void SnapToPoint(Transform point)
    {
        if (_snapCoroutine != null) return;
        _snapCoroutine = StartCoroutine(SnapToPointCoroutine(point));
    }
    public void SnapToPointInstant(Transform point)
    {
        Vector2 pos = point.position; 
        _transform.position = new Vector3(pos.x, pos.y, _transform.position.z);
    }

    public void OnPlayerDeath()
    {
        //TODO
    }

    public void OnPlayerContinue()
    {
        //SnapToPointInstant()
    }


}
