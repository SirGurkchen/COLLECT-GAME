using UnityEngine;

public class RocksLogic : MonoBehaviour, IInteract
{
    [SerializeField] private AudioManager _audioManager;

    public void Interact(PlayerInteract player)
    {
        if (player.GetHeldItem() != null && player.GetHeldItem().GetComponentInParent<ShovelLogic>() != null)
        {
            Destroy(this.gameObject);
            Destroy(player.GetHeldItem());
            _audioManager.PlaySound(AudioManager.SoundType.Digging);
        }
    }
}