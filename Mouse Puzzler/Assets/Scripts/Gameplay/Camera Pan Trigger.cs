using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraPanTrigger : MonoBehaviour, IReset
{
    [SerializeField] private Transform _nextSnapPoint;
    [SerializeField] private Transform _queuedSnapPoint;

    private IPannableCamera cam;
    private bool _isAlternated = false;

    public void Reset()
    {
        if (_isAlternated) SwitchSnapPoints();
    }
    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }

    private void Start()
    {
        cam = Camera.main.GetComponent<IPannableCamera>();
    }

    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;

        if (cam == null) 
        {
            Debug.Log("Cannot Pan Camera. Main Camera has no GameCamera component.", this); 
            return;
        }

        cam.SnapToPoint(_nextSnapPoint);
        SwitchSnapPoints();
    }

    private void SwitchSnapPoints()
    {
        Transform temp = _nextSnapPoint;
        _nextSnapPoint = _queuedSnapPoint;
        _queuedSnapPoint = temp;

        _isAlternated = !_isAlternated;
    }

}
