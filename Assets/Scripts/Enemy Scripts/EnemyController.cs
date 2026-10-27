using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private Transform[] _patrolPoints;
    private int _nextPoint;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    private float _waitTime = 2f;
    private float _waitCounter;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;

        _waitCounter = _waitTime;
    }

    private void FixedUpdate()
    {
        EnemyPatrol();
    }

    private void EnemyPatrol()
    {
        float _distanceToPoint = Mathf.Abs(transform.position.x - _patrolPoints[_nextPoint].position.x);

        if (_distanceToPoint > 0.2f) // if not still reached to the point 
        {
            if (transform.position.x < _patrolPoints[_nextPoint].position.x)
            {
                _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);

            }
            else
            {
                _rb.velocity = new Vector2(-_moveSpeed, _rb.velocity.y);

            }
            //jump to high posiions
            if (transform.position.y < _patrolPoints[_nextPoint].position.y - 0.5f && _rb.velocity.y < 0.1f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }

        }
        else // if point reached
        {
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
            _waitCounter -= Time.deltaTime;

            if (_waitCounter <= 0f) // if waiting time ended
            {
                _nextPoint++;
                if (_nextPoint >= _patrolPoints.Length)
                {
                    _nextPoint = 0;
                }
                _waitCounter = _waitTime;
            }

        }
    }
}
