using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    
    public static UI Instance { get; private set; }

    #region Editor exposed members
    [SerializeField] private Text _mainText;
    [SerializeField] private Text _leftPlayerScoreText;
    [SerializeField] private Text _rightPlayerScoreText;
    #endregion

    void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    
    /// <param name="newMessage">Message text to show</param>
    public void ChangeMainMessage(string newMessage)
    {
        _mainText.text = newMessage;
       
    }

    
    /// <param name="leftPlayerScore">Left player score</param>
    /// <param name="rightPlayerScore">Right player score</param>
    public void UpdatePlayersScores(int leftPlayerScore, int rightPlayerScore)
    {
        _leftPlayerScoreText.text = leftPlayerScore+"";
        _rightPlayerScoreText.text = rightPlayerScore+"";
        
    }
}