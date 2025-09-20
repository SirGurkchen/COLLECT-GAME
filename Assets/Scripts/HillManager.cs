using UnityEngine;

public class HillManager : MonoBehaviour
{
    [SerializeField] private GameObject hillRocks;
    [SerializeField] private GameObject shovel;
    [SerializeField] private HillCoinLogic hillCoin;
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        hillCoin.OnHillCoinCollect += HillCoin_OnHillCoinCollect;
    }

    private void HillCoin_OnHillCoinCollect()
    {
        hillRocks.SetActive(true);
        shovel.SetActive(true);
        audioManager.PlayRockSound();
    }
}