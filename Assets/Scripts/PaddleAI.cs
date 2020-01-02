using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    public float[] speeds = new float[4];
    float speed;

    public Transform target;

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

        Debug.Log("PaddleAI Speed: " + speed);
    }

    void Update()
    {
        FindTarget();
        ChaseTarget();
    }

    void FindTarget()
    {
        if (target == null)
        {
            GameObject search = GameObject.FindGameObjectWithTag("Ball");
            if (search != null)
            {
                target = search.transform;
                Debug.Log("Targeting " + target);
            }
        }
    }

    void ChaseTarget()
    {
        if (target != null)
        {
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(transform.position.y, target.position.y, Time.deltaTime * speed);
            transform.position = newPos;
        }
        else
        {
            Debug.Log("No target");
        }
    }
}
