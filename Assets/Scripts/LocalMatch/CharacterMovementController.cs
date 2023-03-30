using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterMovementController : MonoBehaviour
{
    [Range(0,1)][SerializeField] private int _playerNumber = 0;
    [Header("Characteristics")]
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _jumpForce = 20;
    [SerializeField] private float _downSpeed = 50;
    [SerializeField] private float _maxDownSpeed = 30;
    [Range(0, 9.81f)] [SerializeField] private float _gravityScale = 3.5f;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRender;
    [Header("Boost")]
    [SerializeField] private float _boostTime = 0.3f;
    [SerializeField] private float _boostSpeed = 15f;
    [SerializeField] private float _boostButtonPressingSpeed = 0.19f;
    [Header("Audio")]
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _boostClip;
    [SerializeField] private AudioSource _playerSound;
    [Header("Other")]
    [SerializeField] private GameObject _MobileControlsCanvas;
    

    private KeyCode _upKey;
    private KeyCode _downKey;
    private KeyCode _rightKey;
    private KeyCode _leftKey;

    private ScoreManager _scoreManager;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _inAir;
    private bool _doubleJump;
    private bool _isBoost;
    private float _ballTouchTime;
    private float _rightPressedTime;
    private float _leftPressedTime;
    private bool _mobileUp;
    private bool _mobileDown;
    private bool _mobileRight;
    private bool _mobileLeft;
    private bool _mobileLeftFrame;
    private bool _mobileRightFrame;


    void Start()
    {
        if (Application.isMobilePlatform) _MobileControlsCanvas.SetActive(true);

        _spriteRender.sprite = _sprites[PlayerPrefs.GetInt("Skin", 0)];

        _mobileUp = false;
        _mobileDown = false;
        _mobileRight = false;
        _mobileLeft = false;
        _mobileRightFrame = false;
        _mobileLeftFrame = false;

        if (_playerNumber == 0)
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
            _spriteRender.flipX = true;
        }

        _ballTouchTime = 0;
        _rightPressedTime = 5;
        _leftPressedTime = 5;
        _inAir = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravityScale;
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _playerSound.volume = GameObject.Find("Sounds Source(Clone)").GetComponent<AudioSource>().volume;
        _animator = GetComponent<Animator>();
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
            if (Input.GetKeyDown(_upKey) || _mobileUp)
            {
                if (!_inAir)
                {
                    _jumping = true;
                    _doubleJump = true;
                    _inAir = true;
                    _animator.SetBool("InAir", true);
                    up += _jumpForce;

                    if(_playerNumber == 0) _animator.SetTrigger("RightBoost");
                    else _animator.SetTrigger("LeftBoost");

                    _playerSound.clip = _jumpClip;
                    _playerSound.Play();
                }
                else if (_doubleJump)
                {
                    _jumping = true;
                    _doubleJump = false;
                    up += _jumpForce;

                    if (_playerNumber == 0) _animator.SetTrigger("RightBoost");
                    else _animator.SetTrigger("LeftBoost");

                    _playerSound.clip = _jumpClip;
                    _playerSound.Play();
                }
            }
            if ((Input.GetKey(_downKey) || _mobileDown) && _inAir && !_jumping)
            {
                _movingDown = true;
                if (_rigidbody.velocity.y + (up * Time.deltaTime) > -_maxDownSpeed) up -= _downSpeed;
            }
            if (Input.GetKey(_rightKey) || _mobileRight)
            {
                right += _speed;
            }
            if (Input.GetKey(_leftKey) || _mobileLeft)
            {
                right -= _speed;
            }


            if (Input.GetKeyDown(_rightKey))
            {
                if (_rightPressedTime < _boostButtonPressingSpeed)
                {
                    _animator.SetTrigger("RightBoost");

                    StartCoroutine(SpeedBoost(1));

                    _playerSound.clip = _boostClip;
                    _playerSound.Play();
                }
                _rightPressedTime = 0;
                _mobileRightFrame = false;
            }

            if (Input.GetKeyDown(_leftKey))
            {
                if (_leftPressedTime < _boostButtonPressingSpeed)
                {
                    _animator.SetTrigger("LeftBoost");

                    StartCoroutine(SpeedBoost(0));

                    _playerSound.clip = _boostClip;
                    _playerSound.Play();
                }
                _leftPressedTime = 0;
            }
            _mobileLeftFrame = false;
        }


        if (_jumping)
            _rigidbody.velocity = new Vector2(right, up);
        else if (_movingDown)
            _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y + (up * Time.deltaTime));
        else if (!_isBoost)
        {
            _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y);
        }

        if (right != 0) _animator.SetBool("Move", true);
        else _animator.SetBool("Move", false);

        _mobileUp = false;
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
            if (_ballTouchTime > 2)
            {
                if(_playerNumber == 0)
                    _scoreManager.Score(1);
                else
                    _scoreManager.Score(0);
            }
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

   public void SetMobileUp(bool value)
    {
        _mobileUp = value;
    }
    public void SetMobileUDown(bool value)
    {
        _mobileDown = value;
    }
    public void SetMobileRight(bool value)
    {
        _mobileRight = value;
        _mobileRightFrame = value;
    }
    public void SetMobileLeft(bool value)
    {
        _mobileLeft = value;
        _mobileLeftFrame = value;
    }
}
