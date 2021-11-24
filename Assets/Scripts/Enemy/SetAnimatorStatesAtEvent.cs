using System.Collections;
using UnityEngine;

public class SetAnimatorStatesAtEvent : MonoBehaviour
{
    [SerializeField] public Animator Animator;

    [SerializeField] public string fieldName;

    [SerializeField] public float trueDelay;
    [SerializeField] public float falseDelay;


    public void SetTrue()
    {
        if (null == Animator)
        {
            return;
        }
        
        if(trueDelay > 0)
        {
            StartCoroutine(SetValueWithDelay(trueDelay, true));
        }
        else
        {
            Animator.SetBool(fieldName, true);
        }
        
    }

    public void SetFalse ()
    {
        if (null == Animator)
        {
            return;
        }


        if (falseDelay > 0)
        {
            StartCoroutine(SetValueWithDelay(falseDelay, false));
        }
        else
        {
            Animator.SetBool(fieldName, false);
        }
    }

    private IEnumerator SetValueWithDelay(float delay, bool value)
    {
        yield return new WaitForSeconds(delay);

        Animator.SetBool(fieldName, value);
    }
}

