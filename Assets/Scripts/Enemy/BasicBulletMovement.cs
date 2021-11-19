using DG.Tweening;
using System;

public class BasicBulletMovement : BulletMovement
{
    private DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> tweener;

    protected override void Shooted()
    {
        var duration = (_target.transform.position - transform.position) / _initSpeed;
        tweener = transform.DOMove(_target.transform.position, duration.magnitude).OnComplete(pathCompleted);
    }

    private void pathCompleted()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        tweener.Complete();
    }
}
