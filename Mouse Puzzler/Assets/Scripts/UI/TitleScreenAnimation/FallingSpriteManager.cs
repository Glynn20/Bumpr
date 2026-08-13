using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.U2D;

public class FallingSpriteManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _destroyCollider;
    [SerializeField] private Transform _spriteSpawnPoint;
    [SerializeField] private float _spriteSpawnRangeX = 9.5f;
    [SerializeField] private int _poolSize = 15;
    [SerializeField] private float _spritesPerSecond = .7f;
    [SerializeField] private FallingSpriteData _data;
    private float _timeBeforeSpriteSpawn;
    private List<GameObject> FallingSpritePool = new List<GameObject>();
    bool StopRespawningSprites = false;

    public void OnNewGamePressed()
    {
        StopRespawningSprites = true;
    }

    private void Awake()
    {
        FallingSpritePool.Clear();

        for (int i = 0; i < _poolSize; ++i)
        {
            CreateNewPoolElement();
        }
        _timeBeforeSpriteSpawn = 1 / _spritesPerSecond;
    }

    private void Update()
    {
        _timeBeforeSpriteSpawn -= Time.deltaTime;

        if (_timeBeforeSpriteSpawn > 0) return;

        _timeBeforeSpriteSpawn = 1 / _spritesPerSecond;

        if (!StopRespawningSprites) ActivateNextAvailablePoolObject();
    }

    /* Destroys Falling Sprites at the bottom of the screen */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FallingSprite>() != null)
        {
            DeactivatePoolObject(other.gameObject);
        }
    }

    private void CreateNewPoolElement()
    {
        GameObject newObject = new GameObject("Pool_FallingSprite");
        newObject.transform.SetParent(this.transform);
        FallingSprite fallingSpriteRef = newObject.AddComponent<FallingSprite>();
        //fallingSpriteRef.Initialize(_data);
        FallingSpritePool.Add(newObject);
        newObject.SetActive(false);
    }

    private void ActivateNextAvailablePoolObject()
    {
        for (int i = 0; i < _poolSize; ++i)
        {
            if (FallingSpritePool[i].gameObject.activeSelf == false)
            {
                ConfigureAndPosition(FallingSpritePool[i]);
                return;
            }
        }
    }

    private void ConfigureAndPosition(GameObject fallingSprite)
    {
        fallingSprite.SetActive(true);
        fallingSprite.GetComponent<FallingSprite>().Initialize(_data);
        fallingSprite.transform.position = 
        new Vector2(
            Random.Range((_spriteSpawnPoint.position.x - _spriteSpawnRangeX),(_spriteSpawnPoint.position.x + _spriteSpawnRangeX)),
            _spriteSpawnPoint.position.y
            );
    }
    
    private void DeactivatePoolObject(GameObject obj)
    {
        for (int i = 0; i < _poolSize; ++i)
        {
            if (obj == FallingSpritePool[i])
            {
                FallingSpritePool[i].SetActive(false);
            }
        }
    }
}
