using UnityEngine;
using UnityEngine.Events;

public class DropHitter : MonoBehaviour
{
    [SerializeField] private DropType attractiveType;

    [Header("Events")]
    [SerializeField] private UnityEvent onHitDrop;

    public bool Match(DropType bumpedType)
    {
        return bumpedType == attractiveType;
    }

    public void Hit()
    {
        onHitDrop.Invoke();
    }
}