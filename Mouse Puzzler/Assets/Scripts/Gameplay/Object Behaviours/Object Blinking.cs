using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectBlinking : MonoBehaviour
{
    
    [SerializeField] private float _timeBeforeBlink;
    [SerializeField] private float _blinkDuration;
    [SerializeField] private GameObject[] _controlledObjects;
    private float _remainingTime;
    private WaitForSeconds _blinkWait;
    private Coroutine _blinkCoroutine = null;

    void Awake()
    {
        ActivateObjects();
        _blinkWait = new WaitForSeconds(_blinkDuration);
        _remainingTime = _timeBeforeBlink;
    }

    void Update()
    {
        if (_blinkCoroutine != null) return;

        _remainingTime -= Time.deltaTime;
        
        if (_remainingTime > 0) return;
        
        if (_blinkCoroutine == null)
        {
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
        
    }

    private void ActivateObjects()
    {
        if (_controlledObjects.Length <= 0)
        {
            Debug.Log("Object list in Blinking script not set.", this);
            return;
        }

        foreach (GameObject thing in _controlledObjects)
        {
            thing.SetActive(true);
        }
    }

    private void DeactivateObjects()
    {
        if (_controlledObjects.Length <= 0)
        {
            Debug.Log("Object list in Blinking script not set.", this);
            return;
        }
        foreach (GameObject thing in _controlledObjects)
        {
            thing.SetActive(false);
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        DeactivateObjects();
        yield return _blinkWait;
        ActivateObjects();

        _remainingTime = _timeBeforeBlink;
        _blinkCoroutine = null;
        yield return null;
    }
}
