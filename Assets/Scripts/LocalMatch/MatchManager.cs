using System.Collections;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private static float _startFreezeTime = 1.2f;
    private static GameObject _player0;
    private static GameObject _player1;
    private static GameObject _ball;
    private static Transform _player0Spawn;
    private static Transform _player1Spawn;
    private static Transform _ballPlayer0Spawn;
    private static Transform _ballPlayer1Spawn;

    private void Start()
    {
        _player0 = GameObject.Find("Player0");
        _player1 = GameObject.Find("Player1");
        _ball = GameObject.Find("Ball");
        _player0Spawn = GameObject.Find("Player0Spawn").GetComponent<Transform>();
        _player1Spawn = GameObject.Find("Player1Spawn").GetComponent<Transform>();
        _ballPlayer0Spawn = GameObject.Find("BallPlayer0Spawn").GetComponent<Transform>();
        _ballPlayer1Spawn = GameObject.Find("BallPlayer1Spawn").GetComponent<Transform>();

        ReloadMatch(3);
    }

    protected void ReloadMatch(int player)
    {
        _player0.transform.position = _player0Spawn.position;
        _player0.transform.rotation = _player0Spawn.rotation;

        _player1.transform.position = _player1Spawn.position;
        _player1.transform.rotation = _player1Spawn.rotation;

        if (player == 0)
            _ball.transform.position = _ballPlayer0Spawn.position;
        else if(player == 1)
            _ball.transform.position = _ballPlayer1Spawn.position;


        StartCoroutine(FreezeEverything());
    }

    private IEnumerator FreezeEverything()
    {
        _player0.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _player1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _player0.GetComponent<CharacterMovementController>().enabled = false;
        _player1.GetComponent<CharacterMovementController>().enabled = false;
        _ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(_startFreezeTime);

        _player0.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player0.GetComponent<CharacterMovementController>().enabled = true;
        _player1.GetComponent<CharacterMovementController>().enabled = true;
    }
}
