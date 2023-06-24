using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class DoorAction : MonoBehaviour
{
    [SerializeField] private DoorOpened _doorOpened;
    [SerializeField] private AudioClip _doorSound;
    
    private AudioSource _audioSource;
    private TargetVolumeChanger _alarmSystem;

    private bool _canClose = false;

    private void Start()
    {
        _alarmSystem = _doorOpened.GetComponent<TargetVolumeChanger>();
        _doorOpened.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_canClose)
        {  
            if(_alarmSystem.CurrentVolume == 0)
            {
                _doorOpened.gameObject.SetActive(false);
                _audioSource.PlayOneShot(_doorSound);

                _canClose = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _canClose = false;

            if (_doorOpened.gameObject.activeSelf == false)
            {
                _doorOpened.gameObject.SetActive(true);
                _audioSource.PlayOneShot(_doorSound);
            }            
            
            _alarmSystem.IncreaseVolume();
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _canClose = true;

            _alarmSystem.DecreaseVolume();            
        }
    }
}
