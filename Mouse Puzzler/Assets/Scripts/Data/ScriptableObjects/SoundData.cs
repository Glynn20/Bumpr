using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound Data", menuName = "Data/New Sound Data")]
public class SoundData : ScriptableObject
{
    public AudioClip clip;
    public AudioMixerGroup outputGroup;
    [Range(0.0f, 1.0f)] public float volume = 0.5f;
    [Range(0.1f, 3.0f)] public float pitch = 1.0f;
    [Range(0.0f, 0.5f)] public float pitchVariance;
    public bool looping;
    public bool allowRandomPitch;

    public float GetRandomizedPitch() => pitch + Random.Range(-pitchVariance, pitchVariance);
}
