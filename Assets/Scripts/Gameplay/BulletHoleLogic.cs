using System.Collections;
using UnityEngine;

public class BulletHoleLogic : MonoBehaviour
{
    [SerializeField] private float decayTime = 5f;

    public IEnumerator BulletHoleDecay()
    {
        yield return new WaitForSeconds(decayTime);
        BulletHolePool.Instance.ReturnBullet(this.gameObject);
    }
}