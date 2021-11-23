using UnityEngine;

public class SetAnimatorStateOnTakeDamage : MonoBehaviour
{
    [SerializeField] public Animator Animator;
    [SerializeField] public string fieldName;

    public SetAnimatorStateOnTakeDamage(Health health, float damage)
    {
        Animator.SetBool(fieldName, true);
    }
}
