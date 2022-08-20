using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 150f;

    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector3 Rotation = Input.GetTouch(0).deltaPosition;
            transform.Rotate(0, -Rotation.x * rotationSpeed * Time.deltaTime, 0);
        }
    }
}