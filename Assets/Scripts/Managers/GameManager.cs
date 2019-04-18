using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Private members
    private Ball _ball;
    private int _leftPlayerScore;
    private int _rightPlayerScore;
    #endregion

    #region Editor exposed properties
    [SerializeField] private int _scoreToWin = 3;
    [SerializeField] private int _matchWaitSeconds = 3;
    #endregion

    
    public static GameManager Instance { get; private set; }

    private void Start()
    {
        
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        
        _ball = FindObjectOfType<Ball>();
        if (_ball == null)
        {
            Debug.LogError("Ball not found!");
            Application.Quit();
            return;
        }

        
        UI.Instance.UpdatePlayersScores(_leftPlayerScore, _rightPlayerScore);
        _ball.EnteredEndZone += BallOnEnteredEndZone;

        StartCoroutine(StartNewMatch());
    }

    
    private IEnumerator StartNewMatch()
    {
        _ball.Reset();

        
        if (_matchWaitSeconds <= 0)
        {
            _matchWaitSeconds = 3;
        }

        
        while(_matchWaitSeconds > 0)
        {
            UI.Instance.ChangeMainMessage("" + _matchWaitSeconds);
            yield return new WaitForSeconds(1);
            _matchWaitSeconds--;
        }
        
        UI.Instance.ChangeMainMessage("");
        _ball.GiveRandomVelocity();

        
    }

    
    private void StartNewGame()
    {
        Application.LoadLevel(0);
    }

    
    /// <param name="endZoneType">The goal side that the ball entered</param>
    private void BallOnEnteredEndZone(EndZone.EndZoneType endZoneType)
    {
        StartCoroutine(ShowGoalMessageAndHandleGoal(endZoneType == EndZone.EndZoneType.Left ? Player.PlayerType.Right : Player.PlayerType.Left));
    }

    
    /// <param name="endZoneType">The end zone that was</param>
    /// <returns></returns>
    private IEnumerator ShowGoalMessageAndHandleGoal(Player.PlayerType scoringPlayer)
    {
        
        bool isLeft = scoringPlayer == Player.PlayerType.Left;
        if (isLeft)
        {
            _leftPlayerScore++;
        }
        else
        {
            _rightPlayerScore++;
        }
        
        UI.Instance.UpdatePlayersScores(_leftPlayerScore, _rightPlayerScore);


        
        bool isGameOver = _rightPlayerScore > 2 || _leftPlayerScore > 2;
        if (isGameOver)
        {
            if (isLeft)
            {
                UI.Instance.ChangeMainMessage("Right Player has Won!");
            }
            else
            {
                UI.Instance.ChangeMainMessage("Left Player has Won!");
            }
            
            yield return new WaitForSeconds(3);
            UI.Instance.UpdatePlayersScores(0, 0);
            StartNewGame();
        }
        else
        {
            if (isLeft)
            {
                UI.Instance.ChangeMainMessage("Right Player has Scored!");
            }
            else
            {
                UI.Instance.ChangeMainMessage("Left Player has Scored!");
            }
            yield return new WaitForSeconds(3);
            
            StartCoroutine(StartNewMatch());
        }

        
    }
}