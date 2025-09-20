using System;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    public event Action OnCoinCollect;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatHeight;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        RotateCoin();
        FloatCoin();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCoinCollect?.Invoke();
            Destroy(gameObject);
        }
    }

    private void RotateCoin()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }

    private void FloatCoin()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnDestroy()
    {
        OnCoinCollect = null;
    }
}