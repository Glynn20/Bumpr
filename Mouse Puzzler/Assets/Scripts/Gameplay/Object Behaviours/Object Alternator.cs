using UnityEngine;

public class ObjectAlternator : MonoBehaviour
{

    [System.Serializable]
    public class GameObjectRow
    {
        public GameObject[] row;
    }

    [SerializeField] GameObjectRow[] _objectGroups;
    [SerializeField] private float _intervalTimeInSeconds;
    [Range(0.1f,10f)] private float _timeBeforeNextGroup;
    private int _currentEnabledIndex = 0;

    void Awake()
    {
        _timeBeforeNextGroup =  _intervalTimeInSeconds;
        Initialize();
    }
    void Update()
    {
        _timeBeforeNextGroup -= Time.deltaTime;

        if (_timeBeforeNextGroup > 0f) return;

        EnableNextGroup();
        _timeBeforeNextGroup = _intervalTimeInSeconds;
    }

    void Initialize()
    {
        if (_objectGroups.Length < 2) return;

        for (int i = 0; i < _objectGroups.Length; ++i)
        {
            if (i == 0) 
            {
                EnableObjectsInArray(i); 
                continue;
            }
            else { DisableObjectsInArray(i); }
        }
    }

    void DisableObjectsInArray(int index)
    {
        if (_objectGroups.Length == 0) return;
        if (_objectGroups[index].row.Length == 0) return;

        foreach(GameObject thing in _objectGroups[index].row)
        {
           thing?.gameObject.SetActive(false);
        }
    }

    void EnableObjectsInArray(int index)
    {
        if (_objectGroups.Length == 0) return;
        if (_objectGroups[index].row.Length == 0) return;

        foreach(GameObject thing in _objectGroups[index].row)
        {
           thing?.gameObject.SetActive(true);
        }
    }

    void EnableNextGroup()
    {
        if (_objectGroups.Length == 0) return;

        if (_objectGroups[_currentEnabledIndex].row.Length > 0)
        {
            DisableObjectsInArray(_currentEnabledIndex);
        }

        if (_currentEnabledIndex == _objectGroups.Length - 1)
        {
            _currentEnabledIndex = 0;
        }
        else
        {
            ++_currentEnabledIndex;
        }
        if (_objectGroups[_currentEnabledIndex].row.Length == 0) return;
        EnableObjectsInArray(_currentEnabledIndex);
    }
}
