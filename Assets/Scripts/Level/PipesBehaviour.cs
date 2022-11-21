using UnityEngine;

public class PipesBehaviour : MonoBehaviour
{
    private static readonly float PIPE_MOVE_SPEED = 3f;
    private static readonly float DESTROY_PIPE_X = -5f;

    private Rigidbody2D _rigidbody;
    private bool _isMoving = true;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (!_isMoving)
        {
            _rigidbody.simulated = false;
            return;
        }
        _rigidbody.velocity = Vector2.left * PIPE_MOVE_SPEED;

        if (transform.position.x <= DESTROY_PIPE_X)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == Constants.PLAYER_NAME)
        {
            GameObject.FindObjectOfType<GameController>().AddPoint();
        }
    }

    public void SetIsMoving(bool value) => _isMoving = value;
}
