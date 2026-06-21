using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    // A dictionary that links a specific ParticleData to its own unique pool list
    private Dictionary<ParticleData, List<ParticleSystem>> _pools = new Dictionary<ParticleData, List<ParticleSystem>>();

    /// <summary>
    /// Your event listener method matching your AudioManager signature.
    /// Expects: data[0] = ParticleData, data[1] = Vector2 (position)
    /// </summary>
    public void PlayFX(Component sender, params object[] data)
    {
        if (data != null && data.Length >= 2 
            && data[0] is ParticleData fxData 
            && data[1] is Vector2 position)
        {
            PlayParticleEffect(fxData, position);
        }
    }

    private void PlayParticleEffect(ParticleData fxData, Vector2 position)
    {
        // 1. If this specific effect doesn't have a pool yet, build it!
        if (!_pools.ContainsKey(fxData))
        {
            CreateNewPool(fxData);
        }

        List<ParticleSystem> targetPool = _pools[fxData];

        // 2. Look for an available particle system in this specific pool
        for (int i = 0; i < targetPool.Count; ++i)
        {
            if (targetPool[i].isPlaying == false)
            {
                // Set the position (leaving Z at 0 for 2D space)
                targetPool[i].transform.position = new Vector3(position.x, position.y, 0f);
                targetPool[i].Play();
                return;
            }
        }

        // 3. Fallback: If the pool is completely full, grow it dynamically by 1
        ParticleSystem extraSystem = InstantiateNewElement(fxData);
        extraSystem.transform.position = new Vector3(position.x, position.y, 0f);
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