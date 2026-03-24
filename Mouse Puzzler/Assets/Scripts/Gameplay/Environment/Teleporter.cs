using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IPlayer>() == null) return;
        if (_destination == null)
        {
            Debug.LogWarning("teleport destination not set.", this);
            return;
        }
        other.gameObject.transform.position = _destination.position;
    }
}
