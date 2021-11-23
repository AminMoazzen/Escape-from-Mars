using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;

    private AudioSource _audioSource;

    public void Play()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = clips[Random.Range(0, clips.Length)];
        _audioSource.Play();
    }
}