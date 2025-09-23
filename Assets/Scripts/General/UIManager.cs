using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsCollected;
    [SerializeField] private TextMeshProUGUI ammoText;


    private void Start()
    {
        coinsCollected.text = "Collected Coins: 0 / 10";
    }

    public void RefreshCollectedCoins(int newCoins)
    {
        coinsCollected.text = "Collected Coins: " + newCoins + " / 10";
    }

    public void RefreshAmmo(int currentAmmo)
    {
        ammoText.text = currentAmmo + " / 7";
    }

    public void DeactivateAmmo()
    {
        ammoText.text = string.Empty;
    }
}