using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 150;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            transform.Rotate(0, -mouseX * rotationSpeed * Time.deltaTime, 0);
        }
    }
}
