using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MatchManager
{
    [SerializeField] private int _winScore;
    [SerializeField] private TMP_Text _player0ScoreText;
    [SerializeField] private TMP_Text _player1ScoreText;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _restartText;


    private bool _isGoing;
    private int _scorePlaye0;
    private int _scorePlaye1;

    private void Start()
    {
        _isGoing = true;
        _scorePlaye0 = 0;
        _scorePlaye1 = 0;
    }

    public void Score(int player)
    {
        if (_isGoing)
        {
            if (player == 0)
                _scorePlaye0++;
            else if (player == 1)
                _scorePlaye1++;

            _player0ScoreText.text = _scorePlaye0.ToString();
            _player1ScoreText.text = _scorePlaye1.ToString();

            if (_scorePlaye0 >= _winScore)
                PlayerWin(0);
            else if (_scorePlaye1 >= _winScore)
                PlayerWin(1);

            ReloadMatch(player);
        }
    }

    private void PlayerWin(int player)
    {
        if (player == 0) _winText.text = "Игрок слева выиграл!";
        else _winText.text = "Игрок справа выиграл!";

        _winText.transform.parent.gameObject.SetActive(true);

        _isGoing = false;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        float time = 5;
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;

            if(time == 1) _restartText.text = $"Начало через {time} секунду";
            else if(time >= 2 && time <= 3) _restartText.text = $"Начало через {time} секунды";
            else _restartText.text = $"Начало через {time} секунд";
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
