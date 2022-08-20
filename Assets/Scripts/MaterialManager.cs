using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType
{
	UnSafe,
	Safe,
	Shatter,
	Fever,
}

public class MaterialManager : MonoBehaviour
{
	[SerializeField] private Material[] gameMaterial;

	[SerializeField] private Material unsafeColor;
	[SerializeField] private Material safeColor;
	[SerializeField] private Material shatterColor;
	[SerializeField] private Material feverColor;

	public static MaterialManager Instance { get; private set; }
	public Material FeverColor { get => feverColor; set => feverColor = value; }
	public Material ShatterColor { get => shatterColor; set => shatterColor = value; }

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

	public Material GetMaterial(MaterialType materialType)
	{
		return gameMaterial[(int)materialType];
	}
}
