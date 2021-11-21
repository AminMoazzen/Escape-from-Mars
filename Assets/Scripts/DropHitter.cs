using UnityEngine;
using UnityEngine.Events;

public class DropHitter : MonoBehaviour
{
    [SerializeField] private DropType attractiveType;

    [Header("Events")]
    [SerializeField] private UnityEvent onHitDrop;

    [Header("Events")]
    [SerializeField] private UnityEvent<GameObject> onEnterToDrop;

    [Header("Events")]
    [SerializeField] private UnityEvent<GameObject> onExitFromDrop;

    public bool Match(DropType bumpedType)
    {
        return bumpedType == attractiveType;
    }

    public void Hit()
    {
        onHitDrop.Invoke();
    }

    public void EnterToDrop(GameObject drop)
    {
        onEnterToDrop.Invoke(drop);
    }

    public void ExitedFromDrop(GameObject drop)
    {
        onExitFromDrop.Invoke(drop);
    }
}