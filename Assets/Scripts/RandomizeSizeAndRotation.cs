using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSizeAndRotation : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float minSize = 1;
    [Range(0, 1)] [SerializeField] private float maxSize = 1;
    [SerializeField] private bool randomizeRotation;

    private float _minSize;
    private float _maxSize;

    private void Start()
    {
        Transform trans = transform;
        _minSize = trans.localScale.x * minSize;
        _maxSize = trans.localScale.x * maxSize;

        float randomSize = Random.Range(_minSize, _maxSize);

        trans.localScale = new Vector3(randomSize, randomSize, randomSize);

        if (randomizeRotation)
            trans.rotation = Quaternion.Euler(trans.rotation.x, trans.rotation.y, Random.Range(0, 360));
    }
}