using System.Collections.Generic;
using UnityEngine;

public class BulletHolePool : MonoBehaviour
{
    public static BulletHolePool Instance;

    [SerializeField] private GameObject _bulletHole;
    [SerializeField] private int _poolSize = 15;

    private Queue<GameObject> _holeQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CreatePool(_poolSize);
    }

    private void CreatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var obj = Instantiate(_bulletHole);
            obj.SetActive(false);
            _holeQueue.Enqueue(obj);
        }
    }

    public GameObject GetHole()
    {
        if (_holeQueue.Count > 0)
        {
            var hole = _holeQueue.Dequeue();
            hole.SetActive(true);
            StartCoroutine(hole.GetComponent<BulletHoleLogic>().BulletHoleDecay());
            return hole;
        }
        else
        {
            _poolSize++;
            var obj = Instantiate(_bulletHole);
            obj.SetActive(false);
            _holeQueue.Enqueue(obj);
            var hole = _holeQueue.Dequeue();
            hole.SetActive(true);
            StartCoroutine(hole.GetComponent<BulletHoleLogic>().BulletHoleDecay());
            return hole;
        }
    }

    public void ReturnBullet(GameObject obj)
    {
        obj.SetActive(false);
        _holeQueue.Enqueue(obj);
    }
}