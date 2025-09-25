using UnityEngine;

public class HillManager : MonoBehaviour
{
    [SerializeField] private GameObject _hillRocks;
    [SerializeField] private GameObject _shovel;
    [SerializeField] private HillCoinLogic _hillCoin;
    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _hillCoin.OnHillCoinCollect += HillCoin_OnHillCoinCollect;
    }

    private void HillCoin_OnHillCoinCollect()
    {
        _hillRocks.SetActive(true);
        _shovel.SetActive(true);
        _audioManager.PlaySound(AudioManager.SoundType.Rocks);
    }

    private void OnDestroy()
    {
        _hillCoin.OnHillCoinCollect -= HillCoin_OnHillCoinCollect;
    }
}