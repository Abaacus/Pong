using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreWall : MonoBehaviour
{
    public bool isWall1;
    public GameObject ballObject;
    PongMaster pongMaster;

    void Start()
    {
        pongMaster = PongMaster.instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Ball ball))
        {
            Destroy(collision.gameObject);
            if (isWall1)
            {
                pongMaster.intScores[1]++;
            }
            else
            {
                pongMaster.intScores[0]++;
            }

            pongMaster.UpdateScores();

            Instantiate(ballObject);
        }
    }
}
