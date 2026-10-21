using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private Transform[] _patrolPoints;
    private int _nextPoint;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField]private float _jumpForce = 10f;

    private float _waitTime = 2f;
    private float _waitCounter;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;

        _waitCounter = _waitTime;
    }

    void FixedUpdate()
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
