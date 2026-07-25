using UnityEngine;

public class TeleporterOut : MonoBehaviour
{
    [Header("FX")]
    [SerializeField] private ParticleData _teleporterFX;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlayTeleporterParticleFX;

    private Transform _transform;
    
    public void PlayFX()
    {
        OnPlayTeleporterParticleFX?.Raise(this, _teleporterFX, new Vector2(_transform.position.x, _transform.position.y));
    }

    private void Awake()
    {
        _transform = transform;
    }
}
