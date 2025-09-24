using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public event Action<GameObject> OnBucketDestroy;

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float _shootDistance = 20f;
    [SerializeField] private int _maxAmmo = 7;
    [SerializeField] private LayerMask _bucketMask;
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private PlayerInteract _interact;

    private int currentAmmo;

    private const int BUCKET_MASK_NUMBER = 9;

    private void Start()
    {
        currentAmmo = _maxAmmo;
    }

    public void Shoot(AudioManager audio, Camera cam)
    {
        if (currentAmmo > 0)
        {
            audio.PlaySound(AudioManager.SoundType.Shooting);
            currentAmmo--;
            _uiManager.RefreshAmmo(currentAmmo);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, _shootDistance))
            {
                if (hit.collider.gameObject.CompareTag("Bucket"))
                {
                    OnBucketDestroy?.Invoke(hit.collider.gameObject);
                }
                else
                {
                    GameObject hole = BulletHolePool.Instance.GetHole();
                    hole.transform.position = hit.point;
                    hole.transform.rotation = Quaternion.LookRotation(-hit.normal);
                    hole.transform.position = hit.point + hit.normal * 0.01f;
                }
            }
        }
    }

    public void ActivateGunUI()
    {
        _uiManager.RefreshAmmo(currentAmmo);
    }

    public void DestroyWeapon()
    {
        _interact.DestroyPistolPoint();
        _uiManager.DeactivateAmmo();
    }
}