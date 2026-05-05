using UnityEngine;

public class EnableObjectTrigger : MonoBehaviour, IReset
{
    [SerializeField] private GameObject[] _objects;


    public void Reset()
    {
        DisableObjects();
    }

    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }

    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }

    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }

    private void Start()
    {
        DisableObjects();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() != null &&
            _objects.Length > 0
            )
        {
            EnableObjects();
        }
    }

    private void EnableObjects()
    {
        foreach (GameObject obj in _objects)
        {
            obj.SetActive(true);    
        } 
    }

    private void DisableObjects()
    {
        foreach (GameObject obj in _objects)
        {
            obj.SetActive(false);
        }
    }
}
