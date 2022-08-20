using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private GameObject paintSplash;

	public static PrefabManager instance;

	private void Awake()
	{
		instance = this;
	}

	public void Splash(Collision other)
	{
		Vector3 rotation = transform.rotation.eulerAngles;
		rotation.x = -90;
		transform.rotation = Quaternion.Euler(rotation);

		GameObject splash = Instantiate(paintSplash, transform.position, transform.rotation);
		splash.transform.parent = other.gameObject.transform;
		ContactPoint contact = other.contacts[0];
		Vector3 position = contact.point;
		splash.transform.position = position + (Vector3.up * 0.1f);
		Destroy(splash, 7f);
	}
}
