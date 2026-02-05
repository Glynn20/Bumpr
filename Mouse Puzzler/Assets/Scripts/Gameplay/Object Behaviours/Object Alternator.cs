using UnityEngine;

public class ObjectAlternator : MonoBehaviour
{

    [System.Serializable]
    public class GameObjectRow
    {
        public GameObject[] row;
    }

    [SerializeField] GameObjectRow[] _objectJaggedArray;
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
        if (_objectJaggedArray.Length < 2) return;

        for (int i = 0; i < _objectJaggedArray.Length; ++i)
        {
            if (i == 0) {SetObjectsInArray(i, true); continue;}
            SetObjectsInArray(i, false);
        }
    }

    void SetObjectsInArray(int index, bool makeActive)
    {
        if (_objectJaggedArray.Length == 0) return;

        foreach(GameObject thing in _objectJaggedArray[index].row)
        {
           thing?.gameObject.SetActive(makeActive);
        }
    }

    void EnableNextGroup()
    {
        if (_objectJaggedArray.Length == 0) return;

        SetObjectsInArray(_currentEnabledIndex, false);

        if (_currentEnabledIndex == _objectJaggedArray.Length - 1)
        {
            _currentEnabledIndex = 0;
        }
        else
        {
            ++_currentEnabledIndex;
        }

        SetObjectsInArray(_currentEnabledIndex, true);
    }
}
