using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

[System.Serializable]
public class MenuSound
{
    public StartMenuAudioManagerLogic.MenuSoundType menuType;
    public AudioClip clip;
}

public class StartMenuAudioManagerLogic : MonoBehaviour
{
    [SerializeField] private AudioSource _menuSoundSource;
    [SerializeField] private AudioSource _menuMusicSource;
    [SerializeField] private List<MenuSound> _menuSounds;

    private Dictionary<MenuSoundType, AudioClip> _soundDic;

    public enum MenuSoundType
    {
        ButtonClick,
        Music
    }

    private void Awake()
    {
        if (_soundDic == null)
        {
            BuildDictionary();
        }
    }

    private void Start()
    {
        _menuMusicSource.clip = _soundDic[MenuSoundType.Music];
        _menuMusicSource.loop = true;
        _menuMusicSource.Play();
    }

    private void BuildDictionary()
    {
        _soundDic = new Dictionary<MenuSoundType, AudioClip>();
        foreach (MenuSound s in _menuSounds)
        {
            _soundDic[s.menuType] = s.clip;
        }
    }

    public void PlayClickSound()
    {
        _menuSoundSource.PlayOneShot(_soundDic[MenuSoundType.ButtonClick]);
    }
}