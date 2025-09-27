using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BucketLogic : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    public void DestroyBucket(GameObject bucket)
    {
        _audioManager.PlaySound(AudioManager.SoundType.Bucket);
        Destroy(bucket);
    }
}