using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectBlinking : MonoBehaviour, IReset
{
    
    [SerializeField] private float _timeBeforeBlink;
    [SerializeField] private float _blinkDuration;
    [SerializeField] private GameObject[] _controlledObjects;
    private float _remainingTime;
    private WaitForSeconds _blinkWait;
    private Coroutine _blinkCoroutine = null;

 public void Reset()
    {
        StopAllCoroutines();
        _blinkCoroutine = null;
        ActivateObjects();
        _remainingTime = _timeBeforeBlink;
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

    void Awake()
    {
        if (_controlledObjects == null || _controlledObjects.Length == 0)
        {
            Debug.LogWarning($"[ObjectBlinking] No objects assigned on {gameObject.name}. Script will stay idle.", this);
            return;
        }
        ActivateObjects();
        _blinkWait = new WaitForSeconds(_blinkDuration);
        _remainingTime = _timeBeforeBlink;
    }

    void Update()
    {
        if (_controlledObjects == null || _controlledObjects.Length == 0 || _blinkCoroutine != null) return;

        _remainingTime -= Time.deltaTime;
        
        if (_remainingTime > 0) return;
        
        if (_blinkCoroutine == null)
        {
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
        
    }

    private void ActivateObjects()
    {
        if (_controlledObjects == null)
        {
            Debug.Log("Object list in Blinking script not set.", this);
            return;
        }

        foreach (GameObject thing in _controlledObjects)
        {
            if (thing != null)
            {
                thing.SetActive(true);
            }
            
        }
    }

    private void DeactivateObjects()
    {
        if (_controlledObjects == null)
        {
            Debug.Log("Object list in Blinking script not set.", this);
            return;
        }
        foreach (GameObject thing in _controlledObjects)
        {
            if (thing != null)
            {
                thing.SetActive(false);
            }    
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
