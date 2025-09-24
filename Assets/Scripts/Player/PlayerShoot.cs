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
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Camera _playerCam;

    private int currentAmmo;

    private void Start()
    {
        currentAmmo = _maxAmmo;

        _gameInput.OnShoot += _gameInput_OnShoot;
        _gameInput.OnReloadPress += _gameInput_OnReloadPress;
    }

    private void _gameInput_OnReloadPress()
    {
        currentAmmo = _maxAmmo;
        _uiManager.RefreshAmmo(currentAmmo);
    }

    private void _gameInput_OnShoot()
    {
        if (_interact.GetHeldItem() != null && _interact.GetHeldItem().TryGetComponent<PistolLogic>(out PistolLogic logic))
        {
            if (currentAmmo > 0)
            {
                _audioManager.PlaySound(AudioManager.SoundType.Shooting);
                currentAmmo--;
                _uiManager.RefreshAmmo(currentAmmo);
                if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out RaycastHit hit, _shootDistance))
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

    private void OnDestroy()
    {
        _gameInput.OnShoot -= _gameInput_OnShoot;
        _gameInput.OnReloadPress -= _gameInput_OnReloadPress;
    }
}