using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TargetVolumeChanger : MonoBehaviour
{
    [SerializeField] private float _volumeChangingSpeed;

    private float _startVolume = 0.01f;
    private float _maximumVolume = 1;

    private AudioSource _audioSource;

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
        StartChangeVolume(_maximumVolume);
    }

    public void DecreaseVolume()
    {
        StartChangeVolume(0);
    }

    private void StartChangeVolume(float targetVolume)
    {
        if (_changeVolumeCoroutine != null)
        {
            StopCoroutine(_changeVolumeCoroutine);
        }

        _changeVolumeCoroutine  = StartCoroutine(ChangeVolume(targetVolume));
    }
    
    private IEnumerator ChangeVolume(float targetVolume)
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
