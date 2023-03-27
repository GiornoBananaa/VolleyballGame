using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBallHit : MonoBehaviour
{
    [SerializeField] private int _scorForPlayer;
    [SerializeField] private ScoreManager _scoreManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<BallScript>())
        {
            _scoreManager.Score(_scorForPlayer);
        }
    }
}
