using System.Collections;
using UnityEngine;

public class BulletHoleLogic : MonoBehaviour
{
    [SerializeField] private float _decayTime = 5f;

    public IEnumerator BulletHoleDecay()
    {
        yield return new WaitForSeconds(_decayTime);
        BulletHolePool.Instance.ReturnBullet(this.gameObject);
    }
}