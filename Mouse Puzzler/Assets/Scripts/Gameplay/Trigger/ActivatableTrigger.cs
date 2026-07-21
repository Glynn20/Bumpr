using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ActivatableTrigger : MonoBehaviour, IReset
{
    [SerializeField] private List<GameObject> _activatables;
    [SerializeField] private bool _isDeactivator = false;
    private bool _activated;
    private Collider2D _collider;


    public void Reset()
    {
        _collider.enabled = true;
        _activated = false;
    }

    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IPlayer>() == null) return;   
        if (_activatables.Count == 0) return;
        if (_activated) return;

        foreach (Object obj in _activatables)
        {
            IActivatable[] activatables = obj.GetComponents<IActivatable>();
            if (activatables != null)
            {
                foreach (IActivatable a in activatables)
                {
                    if (_isDeactivator) a.Deactivate();
                    else                a.Activate();
                }
            }
        }
        
        _activated = true;
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
