using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.PlayerLoop;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _cameraObject;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _flagObject;

    [Header("Initial Checkpoint")]
    [SerializeField] private Transform _initialCamPosition;
    [SerializeField] private Transform _initialSpawnPoint;

    private Checkpoint _currentCheckpoint;
    private IPlayer _player;
    private IPannableCamera _mainCamera;
   
    void Awake()
    {
        if (_initialCamPosition == null || _initialSpawnPoint == null)
        {
            Debug.LogError("Initial Checkpoint paramters must be set in inspector", this);
            return;
        }
        _currentCheckpoint = new Checkpoint(_initialCamPosition, _initialSpawnPoint);
    }
    void Start()
    {
        _player = _playerObject?.GetComponentInChildren<IPlayer>();
        _mainCamera = _cameraObject?.GetComponentInChildren<IPannableCamera>();

        if (_mainCamera == null || _player == null)
        {
            Debug.LogWarning("Level Manager missing Player and/or Camera reference.", this);
        }

        MovePlayerAndCamToCheckpoint();
    }

    private void MovePlayerAndCamToCheckpoint()
    {
        _player.TeleportTo(_currentCheckpoint.SpawnPoint);
        _mainCamera.SnapToPointInstant(_currentCheckpoint.CamSnapPoint);
    }
    
    public void OnPlayerDeath()
    {
        // Not Sure yet
    }

    public void OnPlayerContinue()
    {
        MovePlayerAndCamToCheckpoint();
    }

    public void OnCheckpointReached(Component sender, object[] data)
    {
        if (data.Length == 0 || data[0] is not Checkpoint) return;
        
        Checkpoint newCheckpoint = data[0] as Checkpoint;
        
        if (newCheckpoint.CamSnapPoint == null || newCheckpoint.SpawnPoint == null)
        {
            Debug.Log("Invalid Checkpoint received.", this);
        }
        else
        {
            _currentCheckpoint = newCheckpoint;
        }
    }
}
