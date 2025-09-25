using System;
using Unity.VisualScripting;
using UnityEngine;

public class HillCoinLogic : CoinLogic
{
    public event Action OnHillCoinCollect;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnHillCoinCollect?.Invoke();
            base.OnTriggerEnter(other);
        }
    }

    private void OnDestroy()
    {
        OnHillCoinCollect = null;
    }
}