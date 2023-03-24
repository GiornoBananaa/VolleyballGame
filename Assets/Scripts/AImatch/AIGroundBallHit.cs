using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGroundBallHit : MonoBehaviour
{
    [SerializeField] private int _player;
    [SerializeField] private AIScoreManager _scoreManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<BallScript>())
        {
            _scoreManager.Score(_player);
        }
    }
}
