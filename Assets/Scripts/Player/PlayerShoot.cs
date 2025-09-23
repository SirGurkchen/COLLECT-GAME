using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float _shootDistance = 20f;
    [SerializeField] private int _maxAmmo = 7;

    private int currentAmmo;

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
                GameObject hole = BulletHolePool.Instance.GetHole();
                hole.transform.position = hit.point;
                hole.transform.rotation = Quaternion.LookRotation(-hit.normal);
                hole.transform.position = hit.point + hit.normal * 0.01f;
            }
        }
    }

    public void ActivateGunUI()
    {
        _uiManager.RefreshAmmo(currentAmmo);
    }
}