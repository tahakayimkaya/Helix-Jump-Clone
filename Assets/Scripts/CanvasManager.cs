using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public enum Result
{
    Lose,
    Win,
}

public class CanvasManager : MonoBehaviour
{
    private bool isLevelPassed;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelPassedPanel;

    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private Slider gameProgressSlider;

    private int currentLevelIndex;

    private ScoreManager scoreManager;
    private HelixManager helixManager;

    public static CanvasManager Instance { get; private set; }
	public bool IsLevelPassed { get => isLevelPassed; set => isLevelPassed = value; }
	public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }

	private void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt(Variables.CURRENT_LEVEL_INDEX, 1);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

	void Start()
    {
        LevelCount();
        scoreManager = ScoreManager.Instance;
        helixManager = HelixManager.Instance;
        Time.timeScale = 1;
        highScoreText.text = "Best: " + PlayerPrefs.GetInt(Variables.HIGH_SCORE, 0);
    }

    public void LevelPassed()
    {
        PlayerPrefs.SetInt(Variables.CURRENT_LEVEL_INDEX, CurrentLevelIndex + 1);
        SceneManager.LoadScene(Variables.SAMPLE_SCENE);
        HighScoreCheck();
	}

	public void GameOver()
    {
        HighScoreCheck();
        scoreManager.ScoreSystem(ScoreType.ResetScore);
		SceneManager.LoadScene(Variables.SAMPLE_SCENE);
	}

    public void LevelPassedPanel()
	{
        levelPassedPanel.SetActive(true);
    }

    public void GameOverPanel()
	{
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

	public void OpenPanel(Result result)
	{
		switch (result)
		{
            case Result.Lose:
                GameOverPanel();
                break;

            case Result.Win:
                LevelPassedPanel();
                break;
		}
	}

    public void HighScoreCheck()
	{
        if (scoreManager.Score > PlayerPrefs.GetInt(Variables.HIGH_SCORE, 0))
        {
            PlayerPrefs.SetInt(Variables.HIGH_SCORE, scoreManager.Score);
            highScoreText.text = scoreManager.HighScore.ToString();
        }
    }

	public void GameProgress()
    {
		int progress = scoreManager.NumberOfPassedRings * 100 / helixManager.RingCount;
        gameProgressSlider.value = progress;
        scoreText.text = scoreManager.Score.ToString();
    }

	public void LevelCount()
	{
		currentLevelText.text = CurrentLevelIndex.ToString();
		nextLevelText.text = (CurrentLevelIndex + 1).ToString();
	}
}