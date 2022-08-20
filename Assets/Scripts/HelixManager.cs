using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
	[SerializeField] private GameObject helixRing;
	[SerializeField] private GameObject finishRing;
	[SerializeField] private GameObject cylinder;
	
	[SerializeField] private float ySpawn = 27f;
	[SerializeField] private float ringDistance = 2.5f;

	private CanvasManager canvasManager;

	private Scriptable[] scriptable;

	private Vector3 newPosition;
	private Vector3 scaleOfCylinder;

	private int ringCount;

	public static HelixManager Instance;

	public int RingCount { get => ringCount; set => ringCount = value; }

	private void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		scriptable = Resources.LoadAll<Scriptable>(Variables.SCRIPTABLE_OBJECTS);
		canvasManager = CanvasManager.Instance;
		ScriptableLevel();

		for (int i = 0; i < RingCount; i++)
		{
			if (i == 0)
				RingSpawner(false);
			else
				RingSpawner(true);
		}
		Instantiate(finishRing, helixRing.transform.position + newPosition + Vector3.up * -2.5f, transform.rotation);
	}

	public void RingSpawner(bool isRandom)
	{
		GameObject go = Instantiate(helixRing, transform.up * ySpawn, Quaternion.identity);
		go.transform.parent = transform;
		ySpawn -= ringDistance;
		newPosition = go.transform.position;

		if (isRandom)
		{
			go.GetComponent<RingPieces>().SetRandomRing();
		}
		else
		{
			go.GetComponent<RingPieces>().FirstRing();
		}
	}

	private void ScriptableLevel()
	{
		int index = (canvasManager.CurrentLevelIndex % 6) - 1;

		if (canvasManager.CurrentLevelIndex % 6 == 0)
		{
			index = 5;
		}
		RingCount = scriptable[index].ringNumber;
	}
}