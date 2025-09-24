using UnityEngine;

public class MainMenuCoin : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _floatSpeed;
    [SerializeField] private float _floatHeight;

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

    private void RotateCoin()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime, Space.World);
    }

    private void FloatCoin()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * _floatSpeed) * _floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}