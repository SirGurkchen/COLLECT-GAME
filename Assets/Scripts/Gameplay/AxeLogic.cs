using UnityEngine;

public class AxeLogic : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _outline;

    public void Interact(PlayerInteract player)
    {
        if (player.GetHeldItem() == null)
        {
            player.HoldObject(this.gameObject);
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