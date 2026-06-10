using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.PlayerLoop;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _cameraObject;
    
    [Header("Events")]
    [SerializeField] private GameEvent OnLevelBegin;

    [Header("Initial Checkpoint")]
    [SerializeField] private Transform _initialCamPosition;
    [SerializeField] private Transform _initialSpawnPoint;

    private Checkpoint _currentCheckpoint;
    private IPlayer _player;
    private IPannableCamera _mainCamera;
    private static List<IReset> AllResettables = new List<IReset>();

    public void OnPlayerDeath()
    {
        // Not Sure yet
    }

    public void OnPlayerContinue()
    {
        MovePlayerAndCamToCheckpoint();
        ResetAllResettables();
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

    public void OnLevelComplete()
    {
        AllResettables.Clear();
    }

    public static void RegisterResettable(IReset resettable)
    {
        AllResettables.Add(resettable);
    }
    public static void UnregisterResettable(IReset resettable)
    {
        AllResettables.Remove(resettable);
    }
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
        OnLevelBegin?.Raise(this, null);
    }

    private void MovePlayerAndCamToCheckpoint()
    {
        _player.TeleportTo(_currentCheckpoint.SpawnPoint);
        _mainCamera.SnapToPointInstant(_currentCheckpoint.CamSnapPoint);
    }
    
    private void ResetAllResettables()
    {
        List<IReset> resettablesCopyList = new List<IReset>(AllResettables);

        foreach (IReset r in resettablesCopyList)
        {
            if (r != null)
            {
                r.Reset();
            }
        }
    }
}
