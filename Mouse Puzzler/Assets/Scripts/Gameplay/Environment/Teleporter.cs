using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    
    [Header("Sound")]
    [SerializeField] private SoundData _teleporterUsedSoundData;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IPlayer>() == null) return;
        
        if (_destination == null)
        {
            Debug.LogWarning("teleport destination not set.", this);
            return;
        }
        
        other.gameObject.transform.position = _destination.position;
        OnPlaySound?.Raise(this, _teleporterUsedSoundData);
    }
}
