using UnityEngine;

public class RocksLogic : MonoBehaviour, IInteract
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _outline;

    public void Interact(PlayerInteract player)
    {
        if (player.GetHeldItem() != null && player.GetHeldItem().GetComponentInParent<ShovelLogic>() != null)
        {
            Destroy(this.gameObject);
            Destroy(player.GetHeldItem());
            _audioManager.PlaySound(AudioManager.SoundType.Digging);
        }
    }

    public void ShowOutline()
    {
        _outline.SetActive(true);
    }

    public void HideOutline()
    {
        _outline.SetActive(false);
    }
}