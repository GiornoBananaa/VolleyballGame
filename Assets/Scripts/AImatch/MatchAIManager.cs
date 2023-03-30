using System.Collections;
using UnityEngine;
using TMPro;

public class MatchAIManager : MonoBehaviour
{
    [SerializeField] private GameObject _countDownCanvas;

    private static int _startFreezeTime = 2;
    private static GameObject _player0;
    private static GameObject _player1;
    private static GameObject _ball;
    private static Transform _player0Spawn;
    private static Transform _player1Spawn;
    private static Transform _ballPlayer0Spawn;
    private static Transform _ballPlayer1Spawn;


    private void Start()
    {
        _player0 = GameObject.Find("Player");
        _player1 = GameObject.Find("PlayerAI");
        _ball = GameObject.Find("Ball");
        _player0Spawn = GameObject.Find("Player0Spawn").GetComponent<Transform>();
        _player1Spawn = GameObject.Find("Player1Spawn").GetComponent<Transform>();
        _ballPlayer0Spawn = GameObject.Find("BallPlayer0Spawn").GetComponent<Transform>();
        _ballPlayer1Spawn = GameObject.Find("BallPlayer1Spawn").GetComponent<Transform>();

        _ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    protected void ReloadMatch(int player)
    {
        _player0.transform.position = _player0Spawn.position;
        _player0.transform.rotation = _player0Spawn.rotation;

        _player1.transform.position = _player1Spawn.position;
        _player1.transform.rotation = _player1Spawn.rotation;

        if (player == 0)
            _ball.transform.position = _ballPlayer0Spawn.position;
        else if (player == 1)
            _ball.transform.position = _ballPlayer1Spawn.position;


        StartCoroutine(FreezeEverything());
    }

    private IEnumerator FreezeEverything()
    {
        _player0.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _player1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _player0.GetComponent<CharacterMovementController>().enabled = false;
        _player1.GetComponent<AIMovementController>().enabled = false;
        yield return new WaitForEndOfFrame();
        _ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        int time = _startFreezeTime;

        TMP_Text _countDownText = _countDownCanvas.transform.GetComponentInChildren<TMP_Text>();

        _countDownCanvas.SetActive(true);
        _countDownText.text = time.ToString();
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;

            if (time == 1) _countDownText.text = time.ToString();
            else if (time >= 2 && time <= 3) _countDownText.text = time.ToString();
            else _countDownText.text = time.ToString();
        }
        _countDownCanvas.SetActive(false);

        _player0.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player0.GetComponent<CharacterMovementController>().enabled = true;
        _player1.GetComponent<AIMovementController>().enabled = true;
    }
}