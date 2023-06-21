using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    [SerializeField] private DoorOpened _doorOpened;
    [SerializeField] private AlarmSystem _alarmSystem;
    [SerializeField] private AudioClip _doorSound;
    
    private AudioSource _audioSource;

    private bool canClose = false;

    private void Start()
    {
        _alarmSystem = _doorOpened.GetComponent<AlarmSystem>();
        _doorOpened.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canClose)
        {  
            if(_alarmSystem.CurrentVolume() == 0)
            {
                _doorOpened.gameObject.SetActive(false);
                _audioSource.PlayOneShot(_doorSound);

                canClose = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _doorOpened.gameObject.SetActive(true);
            _audioSource.PlayOneShot(_doorSound);
        }        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            canClose = true;
        }
    }
}
