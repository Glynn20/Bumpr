using Unity.VisualScripting;
using UnityEngine;

public class TimedDoor : MonoBehaviour, IActivatable
{
    public bool IsActivatedOnStart {get; set;}
    public bool IsActivated{get; set;}

    [SerializeField] private int _ID;
    [SerializeField] private float _timeBeforeOpen;
    private float _remainingTime;

    void Awake()
    {
        _remainingTime = _timeBeforeOpen;
    }

    void Update()
    {
        if (!IsActivated) return;

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
            if (eventID == _ID) {IsActivated = true;}
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
}
