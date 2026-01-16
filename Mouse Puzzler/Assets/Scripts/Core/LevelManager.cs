using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _flagObject;
    private IPlayer _player;
   
    void Start()
    {
        _player = _playerObject?.GetComponentInChildren<IPlayer>();
        _player.TeleportTo(_spawnPoint);
    }
}
