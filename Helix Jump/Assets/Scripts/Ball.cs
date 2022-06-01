using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody ballRB;
    public float jumpForce = 6;
    public AudioSource bounce, levelPassed, gameOver;

    private void OnCollisionEnter(Collision collision)
    {
        bounce.Play();

        ballRB.velocity = new Vector3(ballRB.velocity.x, jumpForce, ballRB.velocity.z);
        //Debug.Log(collision.transform.GetComponent<MeshRenderer>().material.name);

        if (collision.gameObject.tag == "Unsafe")
        {
            GameController.gameOver = true;
            gameOver.Play();
        }
        else if (collision.gameObject.tag == "Finish" && !GameController.levelPassed)
        {
            GameController.levelPassed = true;
            levelPassed.Play();
        }
    }
}
