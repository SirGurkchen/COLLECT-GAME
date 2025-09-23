using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float _shootDistance = 20f;

    public void Shoot(AudioManager audio, Camera cam)
    {
        audio.PlaySound(AudioManager.SoundType.Shooting);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, _shootDistance))
        {
            GameObject hole = BulletHolePool.Instance.GetHole();
            hole.transform.position = hit.point;
            hole.transform.rotation = Quaternion.LookRotation(-hit.normal);
            hole.transform.position = hit.point + hit.normal * 0.01f;
        }
    }
}