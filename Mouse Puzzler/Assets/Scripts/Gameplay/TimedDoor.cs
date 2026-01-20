using Unity.VisualScripting;
using UnityEngine;

public class TimedDoor : MonoBehaviour
{
    [SerializeField] private int _ID;
    [SerializeField] private float _timeBeforeOpen;
    private float _remainingTime;
    private bool _isActive = false;

    void Awake()
    {
        _remainingTime = _timeBeforeOpen;
    }

    void Update()
    {
        if (!_isActive) return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0)
        {
            Open();
        }
    }
    
    void Open()
    {
        gameObject.SetActive(false);
    }

    public void OnActivateEvent(Component sender, object[] data)
    {
        if (data.Length > 0 && data[0] is int eventID)
        {
            if (eventID == _ID) {_isActive = true;}
        }
    }
}
