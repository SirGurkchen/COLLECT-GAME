using UnityEngine;

public class CutTreeLogic : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _outline;
    [SerializeField] private AudioManager _audioManager;

    public void Interact(PlayerInteract player)
    {
        if (player.GetHeldItem() != null && player.GetHeldItem().GetComponentInParent<AxeLogic>() != null)
        {
            Destroy(this.gameObject);
            Destroy(player.GetHeldItem());
            _audioManager.PlaySound(AudioManager.SoundType.Cutting);
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