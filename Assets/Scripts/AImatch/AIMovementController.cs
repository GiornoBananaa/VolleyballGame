using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementController : MonoBehaviour
{
    [SerializeField] private bool _easyDifficulty;
    [Header("AI movement")]
    [SerializeField] private float _horizontalZone;
    [SerializeField] private float _verticalZone;
    [SerializeField] private float _directionChangeInterval;
    [Header("Characteristics")]
    [SerializeField] private float _speed = 7;
    [SerializeField] private float _jumpForce = 20;
    [Range(0, 9.81f)] [SerializeField] private float _gravityScale = 3.5f;

    private float _xHitCenter;
    private float _directionChangedTime;
    private float _jumpTime;
    private bool _pastDirection;
    private bool _inAir;
    private bool _doubleJump;
    private Rigidbody2D _rigidbody;
    private Rigidbody2D _ballRigidbody;
    private GameObject _ball;


    void Start()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
            _easyDifficulty = true;
        else
            _easyDifficulty = false;

        if (_easyDifficulty)
        {

        }

        _inAir = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravityScale;
        _ball = GameObject.Find("Ball");
        _ballRigidbody = _ball.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _directionChangedTime += Time.deltaTime;
        _jumpTime += Time.deltaTime;

        bool _jumping = false;
        float up = 0;
        float right = 0;

        bool MoveUp = false;
        bool MoveRight = false;
        bool MoveLeft = false;

        _xHitCenter = 1f;


        if (_ball.transform.position.x > 0)
        {
            if (_directionChangedTime > _directionChangeInterval)
            {
                if (_ball.transform.position.x + _xHitCenter - transform.position.x > -_horizontalZone + _xHitCenter/2)
                {
                    MoveRight = true;
                    _directionChangedTime = 0;
                    _pastDirection = true;
                }
                if (_ball.transform.position.x + _xHitCenter - transform.position.x < _horizontalZone - _xHitCenter / 2 && transform.position.x > 3.25)
                {
                    MoveLeft = true;
                    _directionChangedTime = 0;
                    _pastDirection = false;
                }
            }
            else
            {
                if(_pastDirection) MoveRight = true;
                else
                    MoveLeft = true;
            }
            if (_ball.transform.position.y - transform.position.y < _verticalZone && _ball.transform.position.y - transform.position.y > 0.1)
                MoveUp = true;
        }

        if (MoveUp)
        {
            if (!_inAir)
            {
                _jumping = true;
                _doubleJump = true;
                _inAir = true;
                up += _jumpForce;
            }
            else if (_doubleJump && _jumpTime > 0.5f && _ball.transform.position.y - transform.position.y > _verticalZone / 2)
            {
                Debug.Log("hello");
                _jumping = true;
                _doubleJump = false;
                up += _jumpForce;
            }
        }
        if (MoveRight)
        {
            right += _speed;
        }
        if (MoveLeft)
        {
            right -= _speed;
        }


        if (_jumping)
            _rigidbody.velocity = new Vector2(right, up);
        else
            if(right != 0) _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y);

        if (_ball.transform.position.x - transform.position.x < _horizontalZone && _ball.transform.position.x - transform.position.x > -_horizontalZone)
        {
            if(_ballRigidbody.velocity.x <= _speed) _rigidbody.velocity = new Vector2(_ballRigidbody.velocity.x, _rigidbody.velocity.y);
           else _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            _inAir = false;
            _doubleJump = false;
        }
    }
}
