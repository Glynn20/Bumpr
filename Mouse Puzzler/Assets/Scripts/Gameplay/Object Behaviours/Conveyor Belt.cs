using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour, IReset
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform[] _path;
    [Range(0.05f, 10.0f)][SerializeField] private float _spawninterval = 1.0f;
    [Range(0.01f,10.0f)][SerializeField] private float _timeBetweenMove = 1.0f;
    [Range(0.01f,10.0f)][SerializeField] private float _oscillationDuration = 1.0f;

    List<GameObject> _activeConveyorBeltObjects = new List<GameObject>();
    private float _remainingSpawnInterval;

    public void Reset()
    {
        foreach (GameObject go in _activeConveyorBeltObjects)
        {
            if (go != null)
            {
                go.transform.DOKill();
                Destroy(go);    
            }
        }
        _activeConveyorBeltObjects.Clear();
        _remainingSpawnInterval = _spawninterval;
    }

    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }

    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }

    private void OnEnable() { RegisterToManager(); }
    private void OnDisable() { UnregisterFromManager(); }

    private void Awake()
    {
        _remainingSpawnInterval = _spawninterval;
        
        if (_path == null ||
            _prefab == null ||
            _path.Length < 2
        )
        Debug.LogWarning("Conveyor Belt value error.", this);
    }
    
    private void Update()
    {
        _remainingSpawnInterval -= Time.deltaTime;
        if (_remainingSpawnInterval > 0.0f) return;

        SpawnNextConveyorObject();
        _remainingSpawnInterval = _spawninterval;
    }

    private void SpawnNextConveyorObject()
    {
        _activeConveyorBeltObjects.RemoveAll(obj => obj == null);

        GameObject nextObject = Instantiate(_prefab, _path[0].position, _path[0].rotation);
        _activeConveyorBeltObjects.Add(nextObject);

        ObjectPathMovement component = nextObject.GetOrAddComponent<ObjectPathMovement>();

        component.TimeBetweenMove = _timeBetweenMove;
        component.OscillationDuration = _oscillationDuration;

        component.SetNewPath(_path);
        component.DespawnAtLastPoint = true;
        component.Activate();

    }

}
