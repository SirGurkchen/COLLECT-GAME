using UnityEngine;

public class MainMenuCoin : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _floatSpeed;
    [SerializeField] private float _floatHeight;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
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
        float newY = _startPos.y + Mathf.Sin(Time.time * _floatSpeed) * _floatHeight;
        transform.position = new Vector3(_startPos.x, newY, _startPos.z);
    }
}