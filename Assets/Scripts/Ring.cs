using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface IRing
{
	void OnBallInteract(Action<Color> OnBallPassed, bool onFeverMode);

	void CollisionEnter(Action<bool> ParticleActive,Collision other);

	void TriggerEnter(Action<bool> ParticleActive);
}

public enum RingType
{
	Red,
	Black,
	Trigger,
	Finish,
}

public class Ring : MonoBehaviour, IRing
{
	[SerializeField] private RingType ringType;

	private int numberOfRing;

	private MeshRenderer meshRenderer;
	private MeshCollider contact;
	private Rigidbody rb;
	private RingPieces mainRing;

	private MaterialManager materialManager;

	private AudioManager audioManager;
	private CanvasManager canvasManager;
	private ScoreManager scoreManager;
	private PrefabManager prefabManager;
	private Ball ball;

	public int NumberOfRing { get => numberOfRing; set => numberOfRing = value; }

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		contact = GetComponent<MeshCollider>();

		mainRing = GetComponentInParent<RingPieces>();

		materialManager = MaterialManager.Instance;
		audioManager = AudioManager.Instance;
		canvasManager = CanvasManager.Instance;
		scoreManager = ScoreManager.Instance;
		prefabManager = PrefabManager.instance;
		ball = Ball.Instance;
	}

	public void BreakPart(bool isColor)
	{
		contact.enabled = false;
		rb = gameObject.AddComponent<Rigidbody>();

		Vector3 dir = (meshRenderer.bounds.center - transform.parent.position).normalized;
		rb.AddForce((dir * 500) + (Vector3.down * 100));

		if (isColor)
			meshRenderer.material = materialManager.GetMaterial(MaterialType.Shatter);

		transform.SetParent(null);
		Destroy(gameObject, 0.5f);
	}

	public void SetType(RingType ringType)
	{
		this.ringType = ringType;

		switch (ringType)
		{
			case RingType.Red:
				meshRenderer.material = materialManager.GetMaterial(MaterialType.UnSafe);
				break;

			case RingType.Black:
				meshRenderer.material = materialManager.GetMaterial(MaterialType.Safe);
				break;

			case RingType.Trigger:
				meshRenderer.enabled = false;
				contact.isTrigger = true;
				transform.position -= Vector3.down * -0.29f;
				break;

			case RingType.Finish:
				break;
		}
	}

	public void OnBallInteract(Action<Color> OnBallPassed, bool onFeverMode)
	{
		if (onFeverMode)
			OnBallPassed(materialManager.FeverColor.color);
		else
			OnBallPassed(materialManager.ShatterColor.color);

		mainRing.Break(onFeverMode);
	}

	public void CollisionEnter(Action<bool> ParticleActive,Collision other)
	{
		switch (ringType)
		{
			case RingType.Red:
				if (scoreManager.PowerCount >= 3)
				{
					mainRing.Break(true);
					scoreManager.ScoreSystem(ScoreType.AddExtraScore);
				}
				else
				{
					audioManager.PlayAudio(Audio.GameOver);
					canvasManager.OpenPanel(Result.Lose);
				}
				break;

			case RingType.Black:
				if (scoreManager.PowerCount >= 3)
				{
					mainRing.Break(true);
					scoreManager.ScoreSystem(ScoreType.AddExtraScore);
				}
				else
				{
					if (scoreManager.PowerCount >= 2)
						ParticleActive.Invoke(true);
					else
						ParticleActive.Invoke(false);

					prefabManager.Splash(other);
					scoreManager.PowerCount = 0;
					audioManager.PlayAudio(Audio.Bounce);
					ball.BallRB.velocity = new Vector3(ball.BallRB.velocity.x, ball.JumpForce, ball.BallRB.velocity.z);
				}
				break;

			case RingType.Trigger:
				break;

			case RingType.Finish:
				if(canvasManager.IsLevelPassed == false)
                {
					audioManager.PlayAudio(Audio.LevelPassed);
					canvasManager.IsLevelPassed = true;
				}

				audioManager.PlayAudio(Audio.Bounce);

				ball.BallRB.velocity = new Vector3(ball.BallRB.velocity.x, ball.JumpForce, ball.BallRB.velocity.z);

				canvasManager.OpenPanel(Result.Win);
				ParticleActive(false);
				break;

			default:
				break;
		}
	}

	public void TriggerEnter(Action<bool> ParticleActive)
	{
		switch (ringType)
		{
			case RingType.Red:
				break;

			case RingType.Black:
				break;

			case RingType.Trigger:
				scoreManager.PowerCount++;
				scoreManager.ScoreSystem(ScoreType.AddScore);
				audioManager.PlayAudio(Audio.RingPassed);
				mainRing.Break(false);

				if (scoreManager.PowerCount >= 2)
					ParticleActive.Invoke(true);
				else
					ParticleActive.Invoke(false);

				break;

			case RingType.Finish:
				break;

			default:
				break;
		}
	}
}
