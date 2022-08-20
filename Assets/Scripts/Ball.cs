using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	private Rigidbody ballRB;
	[SerializeField] private float jumpForce = 6;

	[SerializeField] private GameObject paintSplash;
	[SerializeField] private Material shatterColor;

	[SerializeField] private ParticleSystem particleEffect;

	private MeshRenderer meshRenderer;

	private ScoreManager scoreManager;

	public static Ball Instance { get; private set; }
	public Rigidbody BallRB { get => ballRB; set => ballRB = value; }
	public float JumpForce { get => jumpForce; set => jumpForce = value; }
	public ParticleSystem ParticleEffect { get => particleEffect; set => particleEffect = value; }

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

		BallRB = GetComponent<Rigidbody>();
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void Start()
	{
		scoreManager = ScoreManager.Instance;
		ParticleEffect.Stop();
	}

	private void OnTriggerEnter(Collider other)
	{
		other.gameObject.GetComponent<IRing>()?.TriggerEnter(ParticleActive);

		other.GetComponent<IRing>()?.OnBallInteract((color) => meshRenderer.material.color = color, scoreManager.PowerCount >= 2);
	}

	private void OnCollisionEnter(Collision other)
	{
		other.gameObject.GetComponent<IRing>()?.CollisionEnter(ParticleActive, other);
	}

	private void ParticleActive(bool isActive)
	{
		if (isActive)
			particleEffect.Play();
		else
			particleEffect.Stop();
	}
}