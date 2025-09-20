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
    [SerializeField] private AudioSource _coinCollectSource;
    [SerializeField] private AudioSource _walkingAudioSource;
    [SerializeField] private AudioSource _rocksAudioSource;
    [SerializeField] private AudioSource _interactAudioSource;
    [SerializeField] private List<Sound> _sounds;

    private Dictionary<SoundType, AudioClip> _soundDic;

    public enum SoundType
    {
        Collect,
        Walk,
        Run,
        Rocks,
        Pickup,
        Digging
    }

    private void Awake()
    {
        if (_soundDic == null)
        {
            BuildDictionary();
        }
    }

    private void BuildDictionary()
    {
        _soundDic = new Dictionary<SoundType, AudioClip>();
        foreach (Sound s in _sounds)
        {
            _soundDic[s.type] = s.clip;
        }
    }

    public void PlayWalkSound(bool isWalking, bool isRunning)
    {
        if (isWalking)
        {
            if (_walkingAudioSource.clip != _soundDic[SoundType.Walk])
            {
                _walkingAudioSource.clip = _soundDic[SoundType.Walk];
                _walkingAudioSource.loop = true;
                _walkingAudioSource.Play();
            }
        }
        else if (isRunning)
        {
            if (_walkingAudioSource.clip != _soundDic[SoundType.Run])
            {
                _walkingAudioSource.clip = _soundDic[SoundType.Run];
                _walkingAudioSource.loop = true;
                _walkingAudioSource.Play();
            }
        }
        else
        {
            if (_walkingAudioSource.isPlaying)
            {
                _walkingAudioSource.clip = null;
                _walkingAudioSource.Stop();
            }
        }
    }

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Rocks:
                _rocksAudioSource.PlayOneShot(_soundDic[soundType]);
                break;
            case SoundType.Digging:
                _rocksAudioSource.PlayOneShot(_soundDic[soundType]);
                break;
            case SoundType.Pickup:
                _interactAudioSource.PlayOneShot(_soundDic[soundType]);
                break;
            case SoundType.Collect:
                _coinCollectSource.PlayOneShot(_soundDic[soundType]);
                break;
        }
    }
}