using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSizeAndRotation : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float minSize = 1;
    [Range(1, 2)] [SerializeField] private float maxSize = 1;
    [SerializeField] private bool randomizeRotationX;
    [SerializeField] private bool randomizeRotationY;
    [SerializeField] private bool randomizeRotationZ;

    private float _minSize;
    private float _maxSize;
    private float _rotationX;
    private float _rotationY;
    private float _rotationZ;

    private void Start()
    {
        Transform trans = transform;
        _minSize = trans.localScale.x * minSize;
        _maxSize = trans.localScale.x * maxSize;

        float randomSize = Random.Range(_minSize, _maxSize);

        trans.localScale = new Vector3(randomSize, randomSize, randomSize);

        _rotationX = trans.rotation.x;
        _rotationY = trans.rotation.y;
        _rotationZ = trans.rotation.z;

        if (randomizeRotationX)
            _rotationX = Random.Range(0, (int)2) * 180;
        if (randomizeRotationY)
            _rotationY = Random.Range(0, (int)2) * 180;
        if (randomizeRotationZ)
            _rotationZ = Random.Range(0, (int)2) * 180;

        trans.localRotation = Quaternion.Euler(_rotationX, _rotationY, _rotationZ);
    }
}