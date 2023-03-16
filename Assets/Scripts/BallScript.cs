using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [Range(0, 5f)] [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _maxSpeed = 15f;
    [SerializeField] private float _firstHitForce = 8f;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(7,8);

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravityScale;
    }

    void Update()
    {
        if(_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_rigidbody.bodyType == RigidbodyType2D.Static && collision.collider.gameObject.layer != 6)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.velocity = (new Vector2(transform.position.x, transform.position.y) - collision.contacts[0].point) * _firstHitForce;
        }
    }
}
