using UnityEngine;

public class CutTreeLogic : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _outline;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameLogic _gameLogic;

    public void Interact(PlayerInteract player)
    {
        if (player.GetHeldItem() != null && player.GetHeldItem().GetComponentInParent<AxeLogic>() != null)
        {
            GameObject newCoin = Instantiate(_coin, transform.position, transform.rotation);
            CoinLogic coinLogic = newCoin.GetComponentInChildren<CoinLogic>();
            if (coinLogic != null)
            {
                _gameLogic.AddCoin(coinLogic);
            }

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