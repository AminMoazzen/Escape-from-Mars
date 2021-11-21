using UnityEngine;

public class SetAnimatorStatesAtEvent : MonoBehaviour
{
    [SerializeField] public Animator Animator;

    [SerializeField] public string fieldName;

    [SerializeField] public bool newValue;

    public void OnValueChangedAt(GameObject gameObject)
    {
        if(null == Animator)
        {
            return;
        }
        Animator.SetBool(fieldName, newValue);
    }
}

