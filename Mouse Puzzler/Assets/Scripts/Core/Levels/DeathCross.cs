using System.Collections.Generic;
using UnityEngine;

public class DeathCross : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private int _poolLength;

    private List<GameObject> CrossPool = new List<GameObject>();    
    private Vector2 _nextSpawnLocation;

    private void Awake()
    {
        for (int i = 0; i < _poolLength; ++i)
        {
            AddNewPoolElement();
        }
    }

    private void AddNewPoolElement()
    {
        GameObject newObject = Instantiate(_prefab, this.transform);
        CrossPool.Add(newObject);
        newObject.gameObject.SetActive(false);    
    }

    public void SetNextCrossLocation(Component sender, params object[] data) //OnPlayerDeath
    {
        if (data != null && data[0] is DeathInfo info)
        {
            if (info == null) return;

            _nextSpawnLocation = info.DeathLocation.position;
        }
    }

    public void PlaceNextCross() //OnPlayerRespawn
    {
        for (int i = 0; i < _poolLength; ++i)
        {
            if (CrossPool[i].activeSelf == false)
            {
                CrossPool[i].SetActive(true);
                CrossPool[i].transform.position = _nextSpawnLocation;
                return;
            }
        }

    }

    private void ResetAllCrosses() //OnNewLevelLoaded()
    {

        for (int i = 0; i < _poolLength; ++i)
        {
            if (CrossPool[i].activeSelf)
            {
                CrossPool[i].SetActive(false);
            }
        }
    }

}
