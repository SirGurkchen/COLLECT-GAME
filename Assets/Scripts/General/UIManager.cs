using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCollected;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameLogic _gameLogic;


    public void RefreshCollectedCoins(int newCoins)
    {
        _coinsCollected.text = "Collected Coins: " + newCoins + " / " + _gameLogic.GetMaxCoins();
    }

    public void RefreshAmmo(int currentAmmo)
    {
        _ammoText.text = currentAmmo + " / 7";
    }

    public void DeactivateAmmo()
    {
        _ammoText.text = string.Empty;
    }

    public void ShowWinText()
    {
        _winText.SetActive(true);
    }
}