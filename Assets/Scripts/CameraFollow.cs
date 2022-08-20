using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothness = 0.04f;
    
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

	void FixedUpdate()
	{
		Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothness);
		transform.position = newPosition;
	}
}