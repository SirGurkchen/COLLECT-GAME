using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _player;

    private CoinLogic[] coins;
    private BucketLogic[] _buckets;
    private List<BucketLogic> _bucketsList;
    private int collectedCoins = 0;

    private void Start()
    {
        _bucketsList = new List<BucketLogic>();
        _buckets = FindObjectsByType<BucketLogic>(FindObjectsSortMode.None);
        foreach (var bucket in _buckets)
        {
            _bucketsList.Add(bucket);
        }

        coins = FindObjectsByType<CoinLogic>(FindObjectsSortMode.None);
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

    public void DestroyBucket(GameObject hitBucket)
    {
        foreach (var bucket in _bucketsList)
        {
            if (hitBucket == bucket.gameObject)
            {
                bucket.DestroyBucket();
                _bucketsList.Remove(bucket);
                break;
            }
        }
    }
}