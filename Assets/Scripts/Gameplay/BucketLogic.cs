using System.Net.Sockets;
using UnityEngine;

public class BucketLogic : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    public void DestroyBucket()
    {
        _audioManager.PlaySound(AudioManager.SoundType.Bucket);
        Destroy(gameObject);
    }
}