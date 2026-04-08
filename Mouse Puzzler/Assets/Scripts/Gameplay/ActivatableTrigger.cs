using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ActivatableTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> _activatables;
    [SerializeField] private bool _isDeactivator = false;

    void OnTriggerEnter2D(Collider2D other)
    {
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
        
        gameObject.SetActive(false);
    }
}
