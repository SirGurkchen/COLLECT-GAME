using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCollected;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _shotBuckets;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private GameObject _shootPrompts;

    public void RefreshCollectedCoins(int newCoins)
    {
        _coinsCollected.text = "Collected Coins: " + newCoins + " / " + _gameLogic.GetMaxCoins();
    }

    public void ShowShootControls()
    {
        _shootPrompts.SetActive(true);
    }

    public void RefreshAmmo(int currentAmmo)
    {
        _ammoText.text = currentAmmo + " / 7";
    }

    public void DeactivateAmmo()
    {
        _ammoText.text = string.Empty;
        _shootPrompts.SetActive(false);
    }

    public void ShowWinText()
    {
        _winText.SetActive(true);
    }

    public void RefreshShotBuckets(int currentBuckets)
    {
        _shotBuckets.text = "Buckets: " + currentBuckets + " | 5";
    }

    public void DisableShotBuckets()
    {
        _shotBuckets.text = string.Empty;
    }
}