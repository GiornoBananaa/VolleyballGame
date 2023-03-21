using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetGroundBallHit : MonoBehaviour
{
    [SerializeField] private int _player;
    [SerializeField] private NetScoreManager _scoreManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<BallScript>())
        {
            Debug.Log(1);
            _scoreManager.Score(_player);
        }
    }
}
