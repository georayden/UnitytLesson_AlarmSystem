using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Movment : MonoBehaviour
{
    [SerializeField] private float _speed;

    private const string Speed = "Speed";

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = false;
            _animator.SetFloat(Speed, _speed);
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            _animator.SetFloat(Speed, _speed);
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
        else
        {
            _animator.SetFloat(Speed, 0);
        }
    }
}
