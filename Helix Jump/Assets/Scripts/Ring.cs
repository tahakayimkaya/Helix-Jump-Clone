using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Transform ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > ball.position.y)
        {
            GameController.numberOfPassedRings++;
            GameController.score+=10;
            Destroy(gameObject);
        }
    }
}
