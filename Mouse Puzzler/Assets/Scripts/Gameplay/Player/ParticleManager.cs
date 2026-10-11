using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    // A dictionary that links a specific ParticleData to its own unique pool list
    private Dictionary<ParticleData, List<ParticleSystem>> _pools = new Dictionary<ParticleData, List<ParticleSystem>>();

    public void PlayFX(Component sender, params object[] data) // 0 ParticleData, 1 Vector2 position || Optional: 2 float rotation
    {
        if (data != null && data.Length >= 2 && 
            data[0] is ParticleData fxData   && 
            data[1] is Vector2 position)
        {
            float zRotation = 0f;

            // If Rotation is also passed
            if (data.Length > 2 && data[2] is float zRotationValue)
            {
                zRotation = zRotationValue;
            }

            PlayParticleEffect(fxData, position, zRotation);
        }

    }

    private void PlayParticleEffect(ParticleData fxData, Vector2 position, float zRotation = 0f)
    {
        // 1. If this specific effect doesn't have a pool yet, build it!
        if (!_pools.ContainsKey(fxData))
        {
            CreateNewPool(fxData);
        }

        List<ParticleSystem> targetPool = _pools[fxData];

        Quaternion rotation = Quaternion.Euler(0f, 0f, zRotation);
        // 2. Look for an available particle system in this specific pool
        for (int i = 0; i < targetPool.Count; ++i)
        {
            if (targetPool[i].isPlaying == false)
            {
                // Set the position (leaving Z at 0 for 2D space)
                targetPool[i].transform.position = new Vector3(position.x, position.y, 0f);
                targetPool[i].transform.rotation = rotation;
                targetPool[i].Play();
                return;
            }
        }

        // 3. Fallback: If the pool is completely full, grow it dynamically by 1
        ParticleSystem extraSystem = InstantiateNewElement(fxData);
        extraSystem.transform.position = new Vector3(position.x, position.y, zRotation);
        extraSystem.transform.rotation = rotation;
        extraSystem.Play();
        
        targetPool.Add(extraSystem);
    }

    private void CreateNewPool(ParticleData fxData)
    {
        List<ParticleSystem> newPoolList = new List<ParticleSystem>();

        for (int i = 0; i < fxData.defaultPoolSize; ++i)
        {
            newPoolList.Add(InstantiateNewElement(fxData));
        }

        _pools.Add(fxData, newPoolList);
    }

    private ParticleSystem InstantiateNewElement(ParticleData fxData)
    {
        ParticleSystem newSystem = Instantiate(fxData.prefab, this.transform);
        
        var mainModule = newSystem.main;
        mainModule.playOnAwake = false;
        
        newSystem.Stop();
        return newSystem;
    }
}