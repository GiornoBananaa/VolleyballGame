using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 8;
    [SerializeField] private float _jumpForce = 20;
    [SerializeField] private float _downSpeed = 50;
    [SerializeField] private float _gravityScale = 3.5f;

    private Rigidbody2D _rigidbody;
    private bool _inAir;
    private bool _doubleJump;
    private bool _boost;

    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(!_inAir)
            {
                _jumping = true;
                _doubleJump = true;
                _inAir = true;
                up += _jumpForce;
            }
            else if(_doubleJump)
            {
                _jumping = true;
                _doubleJump = false;
                up += _jumpForce;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            _movingDown = true;
            up -= _downSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right += _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            right -= _speed;
        }


        if(_jumping) 
            _rigidbody.velocity = new Vector2(right, up);

        else if(_movingDown) 
            _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y + (up * Time.deltaTime));

        else 
            _rigidbody.velocity = new Vector2(right, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.layer);
        if (collision.collider.gameObject.layer == 6)
        {
            _inAir = false;
            _doubleJump = false;
        }
    }
}
