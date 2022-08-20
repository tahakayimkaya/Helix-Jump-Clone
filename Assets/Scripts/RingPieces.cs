using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class RingPieces : MonoBehaviour
{	
	private int index;
	private int randomNumberRed;
	private int randomNumberEmpty;

	[SerializeField] List<GameObject> childs = new List<GameObject>();

	private void Awake()
	{
		randomNumberRed = Random.Range(1, 4);
		randomNumberEmpty = Random.Range(1, 3);

		for (int n = 0; n < transform.childCount; n++)
		{
			childs.Add(transform.GetChild(n).gameObject);
		}
	}

	public void SetRandomRing()
	{
		for (int j = 0; j < randomNumberRed; j++)
		{
			MakeRed();
		}

		for (int k = 0; k < randomNumberEmpty; k++)
		{
			MakeEmpty();
		}

		MakeBlack();
	}

	public void FirstRing()
	{
		MakeEmpty();
		MakeBlack();
	}

	private void MakeRed()
	{
		index = Random.Range(0, childs.Count);
		GameObject child = childs[index];

		child.GetComponent<Ring>().SetType(RingType.Red);

		childs.RemoveAt(index);
	}

	private void MakeEmpty()
	{
		index = Random.Range(0, childs.Count);
		GameObject child = childs[index];

		child.GetComponent<Ring>().SetType(RingType.Trigger);

		childs.RemoveAt(index);
	}

	private void MakeBlack()
	{
		foreach (var child in childs)
		{
			child.GetComponent<Ring>().SetType(RingType.Black);
		}
	}

	public void Break(bool isColor)
	{
		for (int i = 0; transform.childCount > 0; i++)
		{
			transform.GetChild(0).GetComponent<Ring>().BreakPart(isColor);
		}
		Destroy(gameObject, 0.5f);
	}
}
