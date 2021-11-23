using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleArranger : MonoBehaviour
{
    [SerializeField] private bool isStatic = true;

    private Transform _transform;
    private Renderer _particleSystemRenderer;

    private void Start()
    {
        _transform = transform.root;
        _particleSystemRenderer = GetComponent<ParticleSystem>().GetComponent<SpriteRenderer>();
        _particleSystemRenderer.sortingOrder = Mathf.RoundToInt(_transform.position.z * -100);
        if (!isStatic)
            StartCoroutine(UpdateSortingOrder());
    }

    private IEnumerator UpdateSortingOrder()
    {
        while (true)
        {
            _particleSystemRenderer.sortingOrder = Mathf.RoundToInt(_transform.position.z * -10);
            yield return null;
        }
    }
}