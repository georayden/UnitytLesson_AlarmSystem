using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class TargetVolumeChanger : MonoBehaviour
{
    [SerializeField] private float _volumeChangingSpeed;

    private AudioSource _audioSource;

    private float _startVolume = 0.01f;
    private float _maximumVolume = 1;

    private Coroutine _changeVolumeCoroutine;

    public float CurrentVolume => _audioSource.volume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _audioSource.volume = _startVolume;
        _audioSource.Play();
    }

    private void OnDisable()
    {
        _audioSource.Stop();
    }

    public void IncreaseVolume()
    {
        ChangeVolume(_maximumVolume);
    }

    public void DecreaseVolume()
    {
        ChangeVolume(0);
    }

    private  void ChangeVolume(float targetVolume)
    {
        if (_changeVolumeCoroutine != null)
        {
            StopCoroutine(_changeVolumeCoroutine);
        }

        _changeVolumeCoroutine  = StartCoroutine(ChangeVolumeCoroutine(targetVolume));
    }
    
    private IEnumerator ChangeVolumeCoroutine(float targetVolume)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();

        while (_audioSource.volume != targetVolume)
        {
            float currentVolume = _audioSource.volume;
            _audioSource.volume = Mathf.MoveTowards(currentVolume, targetVolume, _volumeChangingSpeed * Time.deltaTime);

            yield return waitForFixedUpdate;
        }
    }   
}
