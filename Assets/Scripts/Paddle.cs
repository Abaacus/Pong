using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPaddle1;
    public float yLimit = 4f;
    public float[] speeds = new float[4];
    float speed;

    void Start()
    {
        switch (PongMaster.instance.difficulty)
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

        Debug.Log(name + " Speed: " + speed);

        speed /= 40;
    }

    private void Update()
    {
        if (isPaddle1)
        {
            transform.Translate(0f, Input.GetAxis("Vertical2") * speed, 0f);
        }
        else
        {
            transform.Translate(0f, Input.GetAxis("Vertical1") * speed, 0f);
        }

        if (transform.position.y >= yLimit)
        {
            transform.position = new Vector3(transform.position.x, yLimit);
        }
        else if (transform.position.y <= -yLimit)
        {
            transform.position = new Vector3(transform.position.x, -yLimit);
        }
    }
}
