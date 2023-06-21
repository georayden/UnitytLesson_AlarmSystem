using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _volumeChangingSpeed;

    private AudioSource _audioSource;

    private float targetValue = 0;

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
        if (_audioSource.volume != targetValue)
        { 
            float currentVolume = _audioSource.volume;
            _audioSource.volume = Mathf.MoveTowards(currentVolume, targetValue, _volumeChangingSpeed * Time.deltaTime);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            targetValue = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            targetValue = 0;
        }
    }

    public float CurrentVolume() => _audioSource.volume;
}
