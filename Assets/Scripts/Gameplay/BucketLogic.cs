using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BucketLogic : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PlayerShoot _shootLogic;

    public void DestroyBucket(GameObject bucket)
    {
        _audioManager.PlaySound(AudioManager.SoundType.Bucket);
        Destroy(bucket);
    }
}