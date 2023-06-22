using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _volumeChangingSpeed;

    private AudioSource _audioSource;

    private float maximumVolme = 1;
    private float _targetValue = 0;

    public float CurrentVolume => _audioSource.volume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _audioSource.volume = 0.1f;
        _audioSource.Play();
    }

    private void OnDisable()
    {
        _audioSource.Stop();
    }

    private void Update()
    { 
        if (_audioSource.volume != _targetValue)
        { 
            float currentVolume = _audioSource.volume;
            _audioSource.volume = Mathf.MoveTowards(currentVolume, _targetValue, _volumeChangingSpeed * Time.deltaTime);
        } 
    }   

    public void IncreaseVolume()
    {
        _targetValue = maximumVolme;
    }
    public void DecreaseVolume()
    {
        _targetValue = 0;
    }
}
