using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private int _poolSize = 15; 
    private List<AudioSource> AudioSourcePool = new List<AudioSource>();
    

    public void OnAudioTriggerCalled(Component sender, params object[] data)
    {
        if (data.Length == 1 && data[0] is SoundData soundData)
        {
            PlaySoundClip(soundData);
        }
    }

    public void OnStopPlayingClip(Component sender, params object[] data)
    {
        if (data.Length == 1 && data[0] is SoundData soundData)
        {
            StopSoundClip(soundData);
        }
    }

    public void StopAllClips()
    {
        for (int i = 0; i < AudioSourcePool.Count; ++i)
        {
            if (AudioSourcePool[i].isPlaying) 
            {
                AudioSourcePool[i].Stop();
                AudioSourcePool[i].clip = null;
            }
        }
    }
    private void PlaySoundClip(SoundData data)
    {
        for (int i = 0; i < AudioSourcePool.Count; ++i)
        {
            if (AudioSourcePool[i].isPlaying == false)
            {
                Debug.Log($"audio source found at i = {i}");
                ConfigureAndPlay(AudioSourcePool[i], data);
                return;
            }
        }
    }

    private void StopSoundClip(SoundData data)
    {
        for (int i = 0; i < AudioSourcePool.Count; ++i)
        {
            if (AudioSourcePool[i].clip == data.clip)
            {
                AudioSourcePool[i].Stop();
                AudioSourcePool[i].clip = null;
                return;
            }
        }
    }

    private void ConfigureAndPlay(AudioSource source, SoundData data)
    {
        source.clip = data.clip;
        source.outputAudioMixerGroup = data.outputGroup;
        source.volume = data.volume;

        if (data.allowRandomPitch)
        {
            source.pitch = data.GetRandomizedPitch();
        }
        else
        {
            source.pitch = data.pitch;
        }

        source.loop = data.looping;

        source.Play();
    }
    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _poolSize; ++i)
        {
            CreateNewPoolElement();
        }
    }
    
    private void CreateNewPoolElement()
    {
        GameObject newObject = new GameObject("Pool_AudioSource");
        newObject.transform.SetParent(this.transform);

        AudioSource newSource = newObject.AddComponent<AudioSource>();
        newSource.playOnAwake = false;

        AudioSourcePool.Add(newSource);
    }

}
