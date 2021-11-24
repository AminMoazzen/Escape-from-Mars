using UnityEngine;
using UnityEngine.Events;

public class AnimationEventRepublisher : MonoBehaviour
{
    [SerializeField] public UnityEvent OnAnimationEvent;

    public void Republish()
    {
        Debug.Log("On Republish");
        OnAnimationEvent.Invoke();
    }
}
