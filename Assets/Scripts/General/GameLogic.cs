using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _player;

    private int collectedCoins = 0;

    private void Start()
    {
        CoinLogic[] coins = FindObjectsByType<CoinLogic>(FindObjectsSortMode.None);
        foreach (var coin in coins)
        {
            coin.OnCoinCollect += Coin_OnCoinCollect;
        }
    }

    private void Coin_OnCoinCollect()
    {
        collectedCoins++;
        _uiManager.RefreshCollectedCoins(collectedCoins);
        _audioManager.PlaySound(AudioManager.SoundType.Collect);
    }
}