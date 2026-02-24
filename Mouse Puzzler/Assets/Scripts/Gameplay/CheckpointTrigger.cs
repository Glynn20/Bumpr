using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent _onCheckpointReached;
    [SerializeField] private Transform _camSnapPoint;
    [SerializeField] private Transform _spawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;

        if (_camSnapPoint == null || _spawnPoint == null)
        {
            Debug.LogWarning("Invalid Checkpoint. Set transforms in inspector.", this);
            return;
        }
        _onCheckpointReached?.Raise(this, new Checkpoint(_camSnapPoint, _spawnPoint));
        
        Destroy(gameObject, 0.1f);
    }
}
    