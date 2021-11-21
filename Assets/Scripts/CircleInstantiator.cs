using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int number;
    [SerializeField] private float radius;

    private bool _isCreating;

    public void Create()
    {
        if (!_isCreating)
        {
            _isCreating = true;
            float angle = Mathf.PI / number;
            float x = 0;
            float z = 0;
            for (int i = 0; i <= number; i++)
            {
                Vector3 offset = new Vector3(x, 0, z);

                Instantiate(prefab, transform.position + offset, Quaternion.identity);
                x = Mathf.Cos(angle) * radius;
                z = Mathf.Sin(angle) * radius;
            }
        }
    }
}