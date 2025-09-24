using UnityEngine;

public class BucketLogic : MonoBehaviour
{
    [SerializeField] private PlayerShoot _shootLogic;
    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _shootLogic.OnBucketHit += _shootLogic_OnBucketHit;
    }

    private void _shootLogic_OnBucketHit()
    {
        _audioManager.PlaySound(AudioManager.SoundType.Bucket);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _shootLogic.OnBucketHit -= _shootLogic_OnBucketHit;
    }
}