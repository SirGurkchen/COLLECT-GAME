using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioManager.SoundType type;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource coinCollectSource;
    [SerializeField] private AudioSource walkingAudioSource;
    [SerializeField] private List<Sound> sounds;

    private Dictionary<SoundType, AudioClip> soundDic;

    public enum SoundType
    {
        Collect,
        Walk,
        Run
    }

    private void Awake()
    {
        if (soundDic == null)
        {
            BuildDictionary();
        }
    }

    private void BuildDictionary()
    {
        soundDic = new Dictionary<SoundType, AudioClip>();
        foreach (Sound s in sounds)
        {
            soundDic[s.type] = s.clip;
        }
    }

    public void PlayCollectSound()
    {
        coinCollectSource.PlayOneShot(soundDic[SoundType.Collect]);
    }

    public void PlayWalkSound(bool isWalking, bool isRunning)
    {
        if (isWalking)
        {
            if (walkingAudioSource.clip != soundDic[SoundType.Walk])
            {
                walkingAudioSource.clip = soundDic[SoundType.Walk];
                walkingAudioSource.loop = true;
                walkingAudioSource.Play();
            }
        }
        else if (isRunning)
        {
            if (walkingAudioSource.clip != soundDic[SoundType.Run])
            {
                walkingAudioSource.clip = soundDic[SoundType.Run];
                walkingAudioSource.loop = true;
                walkingAudioSource.Play();
            }
        }
        else
        {
            if (walkingAudioSource.isPlaying)
            {
                walkingAudioSource.clip = null;
                walkingAudioSource.Stop();
            }
        }
    }
}