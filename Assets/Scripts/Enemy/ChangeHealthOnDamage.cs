using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthOnDamage : MonoBehaviour
{
    public void TakeDamage(Health health, float damage)
    {
        health.TakeDamage(damage);
    }
}
