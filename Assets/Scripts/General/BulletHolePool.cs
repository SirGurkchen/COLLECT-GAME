using System.Collections.Generic;
using UnityEngine;

public class BulletHolePool : MonoBehaviour
{
    public static BulletHolePool Instance;

    [SerializeField] private GameObject bulletHole;

    [SerializeField] private int poolSize = 15;

    private Queue<GameObject> holeQueue = new Queue<GameObject>();

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

        CreatePool(poolSize);
    }

    private void CreatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var obj = Instantiate(bulletHole);
            obj.SetActive(false);
            holeQueue.Enqueue(obj);
        }
    }

    public GameObject GetHole()
    {
        if (holeQueue.Count > 0)
        {
            var hole = holeQueue.Dequeue();
            hole.SetActive(true);
            StartCoroutine(hole.GetComponent<BulletHoleLogic>().BulletHoleDecay());
            return hole;
        }
        else
        {
            poolSize++;
            var obj = Instantiate(bulletHole);
            obj.SetActive(false);
            holeQueue.Enqueue(obj);
            var hole = holeQueue.Dequeue();
            hole.SetActive(true);
            StartCoroutine(hole.GetComponent<BulletHoleLogic>().BulletHoleDecay());
            return hole;
        }
    }

    public void ReturnBullet(GameObject obj)
    {
        obj.SetActive(false);
        holeQueue.Enqueue(obj);
    }
}