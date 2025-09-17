using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;

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
        uiManager.RefreshCollectedCoins(collectedCoins);
        audioManager.PlayCollectSound();
    }
}