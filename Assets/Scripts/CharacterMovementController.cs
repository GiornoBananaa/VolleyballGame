using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [Range(0,1)][SerializeField] private int _playerNumber = 0;
    [Header("Characteristics")]
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _jumpForce = 20;
    [SerializeField] private float _downSpeed = 50;
    [SerializeField] private float _maxDownSpeed = 30;
    [Range(0, 9.81f)] [SerializeField] private float _gravityScale = 3.5f;
    [Header("Boost")]
    [SerializeField] private float _boostTime = 0.3f;
    [SerializeField] private float _boostSpeed = 15f;
    [SerializeField] private float _boostButtonPressingSpeed = 0.19f;


    private KeyCode _upKey;
    private KeyCode _downKey;
    private KeyCode _rightKey;
    private KeyCode _leftKey;

    private Rigidbody2D _rigidbody;
    private bool _inAir;
    private bool _doubleJump;
    private bool _isBoost;
    private float _rightPressedTime;
    private float _leftPressedTime;


    void Start()
    {
        if(_playerNumber == 0)
        {
            _upKey = KeyCode.W;
            _downKey = KeyCode.S;
            _rightKey = KeyCode.D;
            _leftKey = KeyCode.A;
        }
        else
        {
            _upKey = KeyCode.UpArrow;
            _downKey = KeyCode.DownArrow;
            _rightKey = KeyCode.RightArrow;
            _leftKey = KeyCode.LeftArrow;
        }

        _rightPressedTime = 5;
        _leftPressedTime = 5;
        _inAir = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravityScale;
    }

    void Update()
    {
        bool _jumping = false;
        bool _movingDown = false;
        float up = 0;
        float right = 0;

        _rightPressedTime += Time.deltaTime;
        _leftPressedTime += Time.deltaTime;

        if (!_isBoost)
        {
            if (Input.GetKeyDown(_upKey))
            {
                if (!_inAir)
                {
                    _jumping = true;
                    _doubleJump = true;
                    _inAir = true;
                    up += _jumpForce;
                }
                else if (_doubleJump)
                {
                    _jumping = true;
                    _doubleJump = false;
                    up += _jumpForce;
                }
            }
            if (Input.GetKey(_downKey) && _inAir && !_jumping)
            {
                _movingDown = true;
                if (_rigidbody.velocity.y + (up * Time.deltaTime) > -_maxDownSpeed) up -= _downSpeed;
            }
            if (Input.GetKey(_rightKey))
            {
                right += _speed;
            }
            if (Input.GetKey(_leftKey))
            {
                right -= _speed;
            }


            if (Input.GetKeyDown(_rightKey))
            {
                if (_rightPressedTime < _boostButtonPressingSpeed)
                {
                    StartCoroutine(SpeedBoost(1));
                }
                _rightPressedTime = 0;
            }
            if (Input.GetKeyDown(_leftKey))
            {
                if (_leftPressedTime < _boostButtonPressingSpeed)
                {
                    StartCoroutine(SpeedBoost(0));
                }
                _leftPressedTime = 0;
            }
        }


            if (_jumping)
                _rigidbody.velocity = new Vector2(right, up);
            else if (_movingDown)
                _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y + (up * Time.deltaTime));
            else if(!_isBoost)
                _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            _inAir = false;
            _doubleJump = false;
        }
    }

    IEnumerator SpeedBoost(int direction)
    {
        _isBoost = true;

        switch (direction)
        {
            case 0:
                _rigidbody.velocity = new Vector2(-_boostSpeed, _rigidbody.velocity.y);
                break;
            case 1:
                _rigidbody.velocity = new Vector2(_boostSpeed, _rigidbody.velocity.y);
                break;
        }

        yield return new WaitForSeconds(_boostTime);

        _isBoost = false;
    }
}
