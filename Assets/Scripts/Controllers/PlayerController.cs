using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float ROTATE_RATE = 10f;
    private readonly float JUMP_FORCE = 9f;

    [SerializeField]
    private AudioClip _hitClip;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;

    private bool _isPlaying;
    private bool _isAlive = true;

    private bool _canSwitchDirection;
    private float _newYPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() => _rigidbody.mass = 0;

    private void Update()
    {
        if (!_isPlaying)
        {
            transform.position = Vector2.Lerp(
                new Vector2(-1.25f, 4.8f),
                new Vector2(-1.25f, 4.4f),
                (Mathf.Sin(4f * Time.time) + 1.0f) / 2.0f
            );
            return;
        }

        if (_isAlive)
        {
            var yVelocity = _rigidbody.velocity.y;
            var newRotation = Mathf.Min(Mathf.Max(-90, yVelocity * ROTATE_RATE + 60), 30);

            transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
        }
        else
        {
            _rigidbody.rotation = -90;
        }
    }

    public void Flap()
    {
        if (!_isAlive)
        {
            return;
        }
        if (!_isPlaying)
        {
            _rigidbody.mass = 1;
            _isPlaying = true;
        }
        _animator.Play("FlapFast");

        _rigidbody.velocity += Vector2.up * JUMP_FORCE - _rigidbody.velocity;

        _audioSource.Play();
    }

    public void Kill()
    {
        _isAlive = false;
        _animator.enabled = false;

        _audioSource.clip = _hitClip;
        _audioSource.Play();

        GameObject.FindObjectOfType<GameController>().GameOver();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isAlive && other.gameObject.name == "Ground")
        {
            Kill();
        }
    }
}
