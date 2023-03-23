using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetMatchManager : NetworkManager
{
    private static float _startFreezeTime = 1.2f;
    private static int _playersCount = 0;
    private static NetworkObject _player0;
    private static NetworkObject _player1;
    private static NetworkObject _ball;
    private static Transform _player0Spawn;
    private static Transform _player1Spawn;
    private static Transform _ballPlayer0Spawn;
    private static Transform _ballPlayer1Spawn;


    private void Start()
    {
        Singleton.ConnectionApprovalCallback = ApprovalCheck;

        _ball = GameObject.Find("Ball").GetComponent<NetworkObject>();
        _player0Spawn = GameObject.Find("Player0Spawn").GetComponent<Transform>();
        _player1Spawn = GameObject.Find("Player1Spawn").GetComponent<Transform>();
        _ballPlayer0Spawn = GameObject.Find("BallPlayer0Spawn").GetComponent<Transform>();
        _ballPlayer1Spawn = GameObject.Find("BallPlayer1Spawn").GetComponent<Transform>();
    }
    
    public void SetPlayers(NetworkObject player0, NetworkObject player1)
    {
        _player0 = player0;
        _player1 = player1;
    }

    public void ReloadMatch(int player)
    {
        Debug.Log(_player0.transform.position + " - " + _player0Spawn.position + " | " + _player1.transform.position + " - " + _player1Spawn.position);
        _player0.transform.position = _player0Spawn.position;
        _player0.transform.rotation = _player0Spawn.rotation;

        _player1.transform.position = _player1Spawn.position;
        _player1.transform.rotation = _player1Spawn.rotation;

        if (player == 0)
            _ball.transform.position = _ballPlayer1Spawn.position;
        else if (player == 1)
            _ball.transform.position = _ballPlayer0Spawn.position;


        StartCoroutine(FreezeEverything());
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        if (_playersCount >= 2) response.Approved = false;
    }

    private IEnumerator FreezeEverything()
    {
        if (IsServer)
        {
            _player0.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _player1.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _player0.transform.GetComponent<NetCharacterMovementController>().enabled = false;
            _player1.transform.GetComponent<NetCharacterMovementController>().enabled = false;
            _ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            yield return new WaitForSeconds(_startFreezeTime);

            _player0.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _player1.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _player0.transform.GetComponent<NetCharacterMovementController>().enabled = true;
            _player1.transform.GetComponent<NetCharacterMovementController>().enabled = true;
        }
    }
}
