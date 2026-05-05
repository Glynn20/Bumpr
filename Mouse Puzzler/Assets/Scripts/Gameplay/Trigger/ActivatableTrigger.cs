using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ActivatableTrigger : MonoBehaviour, IReset
{
    [SerializeField] private List<GameObject> _activatables;
    [SerializeField] private bool _isDeactivator = false;

    private Collider2D _collider;


    public void Reset()
    {
        _collider.enabled = true;
    }

    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IPlayer>() == null) return;
        
        if (_activatables.Count == 0) return;
        foreach (Object obj in _activatables)
        {
            IActivatable activatable = obj.GetComponent<IActivatable>();
            if (activatable != null)
            {
                if (_isDeactivator) activatable.Deactivate();
                else                activatable.Activate();
            }
        }
        
        _collider.enabled = false;
    }
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    { 
        UnregisterFromManager();
    }
}
