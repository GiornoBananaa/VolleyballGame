using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMovementController : MonoBehaviour
{
    [Header("AI movement")]
    [SerializeField] private float _horizontalZone;
    [SerializeField] private float _verticalZone;
    [SerializeField] private float _directionChangeInterval;
    [Header("Characteristics")]
    [SerializeField] private float _speed = 7;
    [SerializeField] private float _jumpForce = 20;
    [Range(0, 9.81f)] [SerializeField] private float _gravityScale = 3.5f;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRender;
    [Header("Audio")]
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioSource _playerSound;

    private float _ballTouchTime;
    private float _horizontalMistakeTime;
    private float _horizontalMistakeTiming;
    private float _horizontalMistakeDuration;
    private float _verticalMistakeTime;
    private float _verticalMistakeTiming;
    private float _verticalMistakeDuration;
    private float _xHitCenter;
    private float _directionChangedTime;
    private float _jumpTime;
    private bool _easyDifficulty;
    private bool _horizontalMistake;
    private bool _verticalMistake;
    private bool _pastDirection;
    private bool _inAir;
    private bool _doubleJump;
    private Rigidbody2D _rigidbody;
    private Rigidbody2D _ballRigidbody;
    private Animator _animator;
    private GameObject _ball;
    private static AIScoreManager _scoreManager;


    void Start()
    {
        _spriteRender.sprite = _sprites[Random.Range(0, _sprites.Length)];
        _spriteRender.flipX = true;

        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
            _easyDifficulty = true;
        else
            _easyDifficulty = false;


        _ballTouchTime = 0;

        _horizontalMistake = false;
        _verticalMistake = false;

        if (_easyDifficulty)
        {
            _horizontalMistakeTime = 0;
            _verticalMistakeTime = 0;
        }

        _inAir = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravityScale;
        _ball = GameObject.Find("Ball");
        _ballRigidbody = _ball.GetComponent<Rigidbody2D>();
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<AIScoreManager>();
        _playerSound.volume = GameObject.Find("Sounds Source(Clone)").GetComponent<AudioSource>().volume;
        _animator = GetComponent<Animator>();
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

        if(_easyDifficulty) Mistake();


        if (_ball.transform.position.x > 0)
        {
            if (_ballRigidbody.bodyType == RigidbodyType2D.Static || (_ball.transform.position.x < 2.5f && _ball.transform.position.y < 0.5f && _ball.transform.position.x > 0f && transform.position.x < 2.95f && transform.position.y < 0.5f))
            {
                _jumping = true;
                up = _jumpForce;
            }
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
            if (_ball.transform.position.y - transform.position.y < _verticalZone && Mathf.Abs(Mathf.Abs(_ball.transform.position.y) - Mathf.Abs(transform.position.y)) > 1)
                MoveUp = true;
        }

        if (MoveUp)
        {
            if (!_inAir)
            {
                _jumping = true;
                _doubleJump = true;
                _inAir = true;
                _animator.SetBool("InAir", true);
                up += _jumpForce;

                _animator.SetTrigger("LeftBoost");

                _playerSound.clip = _jumpClip;
                _playerSound.Play();
            }
            else if (_doubleJump && _jumpTime > 0.5f && _ball.transform.position.y - transform.position.y > _verticalZone / 2)
            {
                _jumping = true;
                _doubleJump = false;
                up += _jumpForce;

                _animator.SetTrigger("LeftBoost");

                _playerSound.clip = _jumpClip;
                _playerSound.Play();
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

        if (_horizontalMistake && (_ball.transform.position.x - transform.position.x < 3 || _ball.transform.position.x - transform.position.x > 3))
        {
            right = -right;
        }

        if (_verticalMistake && _ball.transform.position.y - transform.position.y > 3)
        {
            if (_jumping)
            {
                _jumping = false;
            }
            else
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
                    _jumping = true;
                    _doubleJump = false;
                    up += _jumpForce;
                }
            }
        }


        if (_jumping)
            _rigidbody.velocity = new Vector2(right, up);
        else
            if(right != 0) _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y);

        if (right != 0) _animator.SetBool("Move", true);
        else _animator.SetBool("Move", false);

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
            _animator.SetBool("InAir", false);
            _doubleJump = false;
        }
        if (collision.collider.gameObject.layer == 7)
        {
            _ballTouchTime = 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 7)
        {
            _ballTouchTime += Time.deltaTime;
            if(_ballTouchTime > 2)
            {
                _scoreManager.Score(0);
            }
        }
    }

    private void Mistake()
    {
        if (_ball.transform.position.x > 0.5f)
        {
            _verticalMistakeTime += Time.deltaTime;
            _horizontalMistakeTime += Time.deltaTime;
        }

        if (!_horizontalMistake && _horizontalMistakeTime >= _horizontalMistakeTiming)
        {
            _horizontalMistakeTiming = Random.Range(2, 3);
            _horizontalMistakeDuration = Random.Range(0.3f, 1f);
            _horizontalMistakeTime = 0;
            _horizontalMistake = true;
        }
        else if (_horizontalMistake && _horizontalMistakeTime >= _horizontalMistakeDuration)
        {
            _horizontalMistakeTime = 0;
            _horizontalMistake = false;
        }

        if (!_verticalMistake && _verticalMistakeTime >= _verticalMistakeTiming && _ball.transform.position.x > 0.5f)
        {
            _verticalMistakeTiming = Random.Range(2, 3);
            _verticalMistakeDuration = Random.Range(0.3f, 0.8f);

            _verticalMistakeTime = 0;
            _verticalMistake = true;
        }
        else if (_verticalMistake && _verticalMistakeTime >= _verticalMistakeDuration)
        {
            _verticalMistakeTime = 0;
            _verticalMistake = false;
        }
    }
}
