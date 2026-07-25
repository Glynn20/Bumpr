using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private TeleporterOut _destination;
    
    [Header("Sound")]
    [SerializeField] private SoundData _teleporterUsedSoundData;

    [Header("FX")]
    [SerializeField] private ParticleData _teleporterFX;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;

    [SerializeField] private GameEvent OnPlayTeleporterParticleFX;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IPlayer>() == null) return;
        
        if (_destination == null)
        {
            Debug.LogWarning("teleport destination not set.", this);
            return;
        }
        
        other.gameObject.transform.position = _destination.transform.position;
        OnPlaySound?.Raise(this, _teleporterUsedSoundData);
        OnPlayTeleporterParticleFX?.Raise(this, _teleporterFX, new Vector2(_transform.position.x, _transform.position.y));
        _destination.PlayFX();
    }
}
