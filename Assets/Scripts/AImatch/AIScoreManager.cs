using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIScoreManager : MatchAIManager
{
    [SerializeField] private int _winScore;
    [SerializeField] private TMP_Text _player0ScoreText;
    [SerializeField] private TMP_Text _player1ScoreText;


    private int _scorePlaye0;
    private int _scorePlaye1;

    private void Start()
    {
        _scorePlaye0 = 0;
        _scorePlaye1 = 0;
    }
    
    public void Score(int player)
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

    private void PlayerWin(int player)
    {
        Debug.Log("Player " + (player + 1) + " wins");
    }
}
