using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _player;

    private CoinLogic[] _coins;
    private List<CoinLogic> _coinsList;
    private int _collectedCoins = 0;
    private const int COINS_ON_MAP = 5;

    private void Start()
    {
        _coinsList = new List<CoinLogic>();

        _coins = FindObjectsByType<CoinLogic>(FindObjectsSortMode.None);
        foreach (var coin in _coins)
        {
            _coinsList.Add(coin);
            coin.OnCoinCollect += Coin_OnCoinCollect;
        }

        _uiManager.RefreshCollectedCoins(0);
    }

    private void Coin_OnCoinCollect()
    {
        _collectedCoins++;
        _uiManager.RefreshCollectedCoins(_collectedCoins);
        _audioManager.PlaySound(AudioManager.SoundType.Collect);

        if (_collectedCoins >= COINS_ON_MAP)
        {
            _uiManager.ShowWinText();
            Invoke("WinGame", 2f);
        }
    }

    public void AddCoin(CoinLogic coin)
    {
        _coinsList.Add(coin);
        coin.OnCoinCollect += Coin_OnCoinCollect;
    }

    public int GetMaxCoins()
    {
        return COINS_ON_MAP;
    }

    private void WinGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void OnDisable()
    {
        if (_coinsList == null) return;

        foreach (var coin in _coinsList)
        {
            if (coin != null)
                coin.OnCoinCollect -= Coin_OnCoinCollect;
        }
    }
}