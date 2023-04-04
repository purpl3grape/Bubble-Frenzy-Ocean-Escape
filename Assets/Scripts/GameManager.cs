using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text FinalScore;

    [SerializeField] private TMP_Text Health;
    [SerializeField] private TMP_Text CurrentScore;

    public int CurrentScoreVal;
    public int FinalScoreVal;
    public int HealthVal;
    public bool GameOver;
    public bool ConfirmRestart => m_restartTaps >= k_TapsToRestart;
    public int k_TapsToRestart = 2;
    [SerializeField] private int m_restartTaps;

    private void Awake()
    {
        Instance = this;
        m_restartTaps = 0;
    }

    public void SetGameOver(bool val)
    {
        GameOver = val;
        GameOverPanel.SetActive(val);
        FinalScoreVal = CurrentScoreVal;
        FinalScore.text = GameOverPanel.activeSelf ? FinalScoreVal.ToString() : 0.ToString();
    }

    public void SetHealth(int health)
    {
        HealthVal = health;
        Health.text = health.ToString();
    }

    public void SetCurrentScore(int score)
    {
        CurrentScoreVal = score;
        CurrentScore.text = score.ToString();
    }

    public void TapToConfirmRestart()
    {
        m_restartTaps++;
        if (ConfirmRestart) { 
            SceneManager.LoadScene(0); 
        }
    }
}
