using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(Renderer))]
public class UpdateSortingOrderBaseOnZ : MonoBehaviour
{
    [SerializeField] private bool isStatic = true;

    private float _precision = 2;

    private Transform _transform;
    private Renderer _renderer;
    private float _multiplier;

    private void Start()
    {
        _multiplier = -Mathf.Pow(10, _precision);
        _transform = transform;
        _renderer = GetComponent<Renderer>();

        _renderer.sortingOrder = Mathf.RoundToInt(_transform.position.z * _multiplier);

        if (!isStatic)
            StartCoroutine(UpdateSortingOrder());
    }

    private IEnumerator UpdateSortingOrder()
    {
        while (true)
        {
            _renderer.sortingOrder = Mathf.RoundToInt(_transform.position.z * _multiplier);
            yield return null;
        }
    }
}