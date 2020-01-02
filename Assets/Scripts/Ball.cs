using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float[] speeds = new float[4];
    float speed;

    public float pauseTime = 3f;

    PongMaster pongMaster;

    void Start()
    {
        pongMaster = PongMaster.instance;
        switch (pongMaster.difficulty)
        {
            default:
                speed = speeds[1];
                break;

            case Difficulty.easy:
                speed = speeds[0];
                break;

            case Difficulty.medium:
                speed = speeds[1];
                break;

            case Difficulty.hard:
                speed = speeds[2];
                break;

            case Difficulty.ultimate:
                speed = speeds[3];
                break;
        }

        Debug.Log("Ball Speed: " + speed);

        rb = GetComponent<Rigidbody>();
        StartCoroutine(GenerateRandomVelocity());
    }

    IEnumerator GenerateRandomVelocity()
    {
        yield return new WaitForSeconds(pauseTime);

        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.velocity = new Vector3(sx * speed, sy * speed, 0);

        Debug.Log("Ball released");
    }

    void Update()
    {
        if (pongMaster.gameEnded)
        {
            Destroy(this);
        }
    }
}
