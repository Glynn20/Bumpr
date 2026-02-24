using UnityEngine;

public class ActivateObjectEventTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent _onActivateObjects;
    [SerializeField] private int _eventID;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() != null)
        {
            _onActivateObjects?.Raise(this, _eventID);

            Destroy(gameObject);
        }
    }
}
