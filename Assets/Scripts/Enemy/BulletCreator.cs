using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    public void CreateBullet(ShootingIntervalHitedEvent evt)
    {
        if (null != evt.target)
        {
            var bullet = Instantiate(evt.prefab, evt.initTransform.position, evt.initTransform.rotation);

            var bulletMovement = bullet.GetComponent<BulletMovement>();

            bulletMovement.Init(evt.target, evt.target.transform.position - evt.initTransform.position, evt.initSpeed);
        }
    }
}
