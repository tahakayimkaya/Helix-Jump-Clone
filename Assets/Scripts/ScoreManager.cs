using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreType
{
    AddScore,
    AddExtraScore,
    ResetScore,
}

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int numberOfPassedRings;
    private int highScore;
    private int powerCount;

    private CanvasManager canvasManager;

    public static ScoreManager Instance { get; private set; }
	
    public int HighScore { get => highScore; set => highScore = value; }
	public int Score { get => score; set => score = value; }
	public int NumberOfPassedRings { get => numberOfPassedRings; set => numberOfPassedRings = value; }
	public int PowerCount { get => powerCount; set => powerCount = value; }

	private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

	private void Start()
	{
        canvasManager = CanvasManager.Instance;
	}

    public void ScoreSystem(ScoreType scoreType)
	{
		switch (scoreType)
		{
			case ScoreType.AddScore:
                Score += canvasManager.CurrentLevelIndex;
                NumberOfPassedRings++;
                canvasManager.GameProgress();
                break;

			case ScoreType.AddExtraScore:
                Score += 20;
                powerCount = 0;
                break;

			case ScoreType.ResetScore:
                Score = 0;
                break;

			default:
				break;
		}
	}
}
