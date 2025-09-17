using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsCollected;



    private void Start()
    {
        coinsCollected.text = "Collected Coins: 0 / 10";
    }

    public void RefreshCollectedCoins(int newCoins)
    {
        coinsCollected.text = "Collected Coins: " + newCoins + " / 10";
    }
}