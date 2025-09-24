using System.Collections.Generic;
using UnityEngine;

public class BucketManager : MonoBehaviour
{
    [SerializeField] private Transform _bucketCoinSpawnPoint;
    [SerializeField] private GameLogic _logic;
    [SerializeField] private GameObject _coin;
    [SerializeField] private PlayerShoot _shootLogic;

    private BucketLogic[] _buckets;
    private List<BucketLogic> _bucketsList;
    
    private void Start()
    {
        _bucketsList = new List<BucketLogic>();
        _buckets = FindObjectsByType<BucketLogic>(FindObjectsSortMode.None);
        foreach (var bucket in _buckets)
        {
            _bucketsList.Add(bucket);
        }

        _shootLogic.OnBucketDestroy += _shootLogic_OnBucketDestroy;
    }

    private void _shootLogic_OnBucketDestroy(GameObject obj)
    {
        for (int i = 0; i < _bucketsList.Count; i++)
        {
            if (obj == _bucketsList[i].gameObject)
            {
                _bucketsList.RemoveAt(i);
                obj.GetComponentInChildren<BucketLogic>().DestroyBucket(obj);
                break;
            }
        }

        if (_bucketsList.Count <= 0)
        {
            GameObject newCoin = Instantiate(_coin, _bucketCoinSpawnPoint.position, _bucketCoinSpawnPoint.rotation);
            CoinLogic coinLogic = newCoin.GetComponentInChildren<CoinLogic>();
            if (coinLogic != null)
            {
                _logic.AddCoin(coinLogic);
            }

            _shootLogic.DestroyWeapon();
        }
    }
}