using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle = 0, Running = 1, Jumping = 2
    }
    PlayerState _currentState = PlayerState.Idle;

    private Animator _anim;
    private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed = 10f, _jumpForce = 10f;
    private float _maxJump = 2f;
    private float _jumpCount = 0f;

    private bool _grounded = false;
    private float _horizontalInput;
    public LayerMask _groundLayer;
    public Transform _groundCheckPoint;
    private float _groundCheckRayDistance = 0.2f;
    private Vector3 _currentScale;

    public BulletController _bullet;
    public Transform _shootPoint;

    [Header("Dash Setting")]
    [SerializeField] private float _dashSpeed = 25f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCoolDownTimer = 0f;
    [SerializeField] private float _dashCoolDown;
    private bool _isDashing = false;
    private bool _canDash = true;
    private float _originalGravityScale;

    [Header("Ball Sprite Setting")]
    public GameObject _standing, _ball;
    private float _ballCounter;
    private float _waitBecomeBall = 0.5f;
    public Transform _bombPoint;
    public GameObject _bomb;





    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _rb.freezeRotation = true;

    }// Start()

    void Update()
    {
        //left right move input 
        _horizontalInput = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }


        // Dashing
        if (Input.GetKeyDown(KeyCode.E) && _canDash && !_isDashing && _standing.activeSelf)
        {
            StartCoroutine(DashRouting());
        }

        //ground check
        RaycastHit2D _hitGround = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _groundCheckRayDistance, _groundLayer);
        Debug.DrawRay(_groundCheckPoint.position, Vector2.down * _groundCheckRayDistance);

        if (_hitGround.collider != null)
        {
            _grounded = true;
            if (_rb.velocity.y <= 0.01f)
            {
                _jumpCount = 0f;
            }

        }
        else
        {
            _grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_standing.activeSelf)
            {
                Firing();
            }
            else if (_ball.activeSelf)
            {
                BombDrop();
            }


            //if () { _ball.activeSelf}
        }

        //Become a ball
        if (!_ball.activeSelf)
        {
            if (Input.GetAxisRaw("Vertical") < -0.9f) // is near to -1
            {
                _ballCounter -= Time.deltaTime;
                if (_ballCounter <= 0)
                {
                    _ball.SetActive(true);
                    _standing.SetActive(false);
                }
            }
            else
            {
                _ballCounter = _waitBecomeBall;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Vertical") > 0.9f) // is near to +1
            {
                _ballCounter -= Time.deltaTime;
                if (_ballCounter <= 0)
                {
                    _ball.SetActive(false);
                    _standing.SetActive(true);

                }

            }
            else
            {
                _ballCounter = _waitBecomeBall;
            }

        }



        PlayerStateSwitch();

    }// Update()

    private void FixedUpdate()
    {
        if (_isDashing) return;
        PlayerMovement();

    }// FixedUpdate()

    void PlayerMovement()
    {
        _rb.velocity = new Vector2(_horizontalInput * _moveSpeed, _rb.velocity.y);
        _currentScale = transform.localScale;

        if (_horizontalInput > 0)
        {
            _currentScale.x = Mathf.Abs(_currentScale.x);

        }
        else if (_horizontalInput < 0)
        {
            _currentScale.x = -Mathf.Abs(_currentScale.x);
        }
        transform.localScale = _currentScale;


    }// PlayerMovement()

    void PlayerJump()
    {
        if (_jumpCount < _maxJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            _jumpCount++;
        }


        _currentState = PlayerState.Jumping;

    }// PlayerJump()

    private void Firing()
    {
        BulletController _newbullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);

        if (transform.localScale.x > 0)
        {
            _newbullet._moveDirection = Vector2.right;
        }
        else
        {
            _newbullet._moveDirection = Vector2.left;

        }

    }// Firing()

    private void BombDrop()
    {
        Instantiate(_bomb, _bombPoint.position, transform.rotation);
    }

    IEnumerator DashRouting()
    {
        //start dashing
        _isDashing = true;
        _canDash = false;

        _originalGravityScale = _rb.gravityScale;
        _rb.gravityScale = 0f;

        _rb.velocity = new Vector2(_currentScale.x * _dashSpeed, 0f);

        //dash duration
        yield return new WaitForSeconds(_dashDuration);

        _rb.velocity = new Vector2(0f, _rb.velocity.y); //horizontal value reset 
        _rb.gravityScale = _originalGravityScale; //gravity reset
        _isDashing = false;

        _dashCoolDownTimer = _dashCoolDown;
        while (_dashCoolDownTimer > 0)
        {
            _dashCoolDownTimer -= Time.deltaTime;
            yield return null;
        }

        // dash cooldown
        _dashCoolDownTimer = 0f;
        _canDash = true;
        Debug.Log(_canDash);
    }

    void PlayerStateSwitch()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                if (_horizontalInput != 0) _currentState = PlayerState.Running;
                if (!_grounded) _currentState = PlayerState.Jumping;
                break;

            case PlayerState.Running:
                if (_horizontalInput == 0) _currentState = PlayerState.Idle;
                if (!_grounded) _currentState = PlayerState.Jumping;
                break;

            case PlayerState.Jumping:
                if (_grounded)
                {
                    if (_horizontalInput != 0)
                    {
                        _currentState = PlayerState.Running;
                    }
                    else
                    {
                        _currentState = PlayerState.Idle;
                    }
                }
                break;

        }
        _anim.SetInteger("PlayerState", (int)_currentState);
        ///Debug.Log(_currentState);

    }
}
