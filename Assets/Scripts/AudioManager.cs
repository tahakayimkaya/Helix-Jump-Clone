using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Audio
{
	Bounce,
	RingPassed,
	LevelPassed,
	GameOver,
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bounceAudio, levelPassedAudio, gameOverAudio, ringPassedAudio;
    
    public static AudioManager Instance { get; private set; }

    private void Awake()
    { 
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
			Instance = this;
        }
    }

    public void PlayAudio(Audio audio)
	{
		switch (audio)
		{
			case Audio.Bounce:
				bounceAudio.Play();
				break;

			case Audio.RingPassed:
				ringPassedAudio.Play();
				break;

			case Audio.LevelPassed:
				levelPassedAudio.Play();
				break;

			case Audio.GameOver:
				gameOverAudio.Play();
				break;

			default:
				break;
		}
	}
}