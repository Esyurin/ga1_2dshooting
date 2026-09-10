// 관리: 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 게임 로직

using System;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private const string BestScoreSaveKey = "BestScore";

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    private int _bestScore;
    private int _currentScore;
    private int _lastRefreshScore = -1;

    protected override void OnAwake()
    {
        LoadScore();
        Refresh();
    }

    private void LoadScore()
    {
        _bestScore = PlayerPrefs.GetInt(BestScoreSaveKey, 0);
    }

    private void Refresh()
    {
        if (_lastRefreshScore == _currentScore) return;

        _lastRefreshScore = _currentScore;

        _bestScoreTextUI.text = $"Best Score: {_bestScore}";
        _currentScoreTextUI.text = $"Current Score: {_currentScore}";
    }

    public void AddScore(int score)
    {
        if (score <= 0)
        {
            Debug.LogError($"Score {score} is less than or equal to zero");
            return;
        }

        _currentScore += score;

        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            PlayerPrefs.SetInt(BestScoreSaveKey, _bestScore);
            PlayerPrefs.Save();
        }

        Refresh();
    }
}