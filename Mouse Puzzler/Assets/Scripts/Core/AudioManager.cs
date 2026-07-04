using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    public SoundData CurrentMusicTrack { get {return _currentMusicTrackData;} }

    [Header("SFX")]
    [SerializeField] private int _poolSize = 15; 
    private List<AudioSource> AudioSourcePool = new List<AudioSource>();
    
    [Header("BGM")]
    [SerializeField] private SoundData _titleScreenTrackData;
    private AudioSource _BGMSource;
    private SoundData _currentMusicTrackData;
    public void OnLevelLoadComplete(Component sender, params object[] data)
    {
        if (data != null && data.Length == 1 && data[0] is SceneData sceneData)
        {
            if (sceneData.BGMTrack == null)
            {
                StopBGMTrack();
            }
            else
            {
                ChangeBGMTrack(sceneData.BGMTrack);
            }
        }
    }
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

    public void PlayBGMTrack()
    {
        if (_BGMSource.clip != null)
        {
            _BGMSource.Play();
        }    
    }

    public void ChangeBGMTrack(SoundData newTrack)
    {
        _currentMusicTrackData = newTrack;
        StopBGMTrack();
        ConfigureAudioSourceAndData(_BGMSource, newTrack);
        PlayBGMTrack();
    }
    public void StopBGMTrack()
    {
        if (_BGMSource.clip != null)
        {
            _BGMSource.Stop();
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

    protected override void Awake()
    {
        base.Awake();

        if (Instance != this) return;

        for (int i = 0; i < _poolSize; ++i)
        {
            CreateNewPoolElement();
        }
        _BGMSource = GetComponent<AudioSource>();
        ConfigureAudioSourceAndData(_BGMSource, _titleScreenTrackData);
        _currentMusicTrackData = _titleScreenTrackData;
        PlayBGMTrack();
    }

    private void PlaySoundClip(SoundData data)
    {
        for (int i = 0; i < AudioSourcePool.Count; ++i)
        {
            if (AudioSourcePool[i].isPlaying == false)
            {
                ConfigureAndPlay(AudioSourcePool[i], data);
                return;
            }
        }

        CreateNewPoolElement();

        ConfigureAndPlay(AudioSourcePool[AudioSourcePool.Count - 1], data);
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
    private void ConfigureAudioSourceAndData(AudioSource source, SoundData data)
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
