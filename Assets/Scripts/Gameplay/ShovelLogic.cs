using UnityEngine;

public class ShovelLogic : MonoBehaviour, IInteract
{
    public void Interact(PlayerInteract player)
    {
        player.HoldObject(this.gameObject);
    }
}