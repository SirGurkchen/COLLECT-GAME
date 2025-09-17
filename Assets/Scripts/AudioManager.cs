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
    [SerializeField] private List<Sound> sounds;

    private Dictionary<SoundType, AudioClip> soundDic;

    public enum SoundType
    {
        Collect
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
}