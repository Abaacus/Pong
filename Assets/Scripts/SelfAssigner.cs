using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfAssigner : MonoBehaviour
{
    public enum GUIType {timer, p1score, p2score};
    public GUIType guiType;
    public TextMeshProUGUI[] GUIs;
    public GameObject paddle;

    void Awake()
    {
        PongMaster pongMaster = PongMaster.instance;

        pongMaster.timerGUI = GUIs[0];
        pongMaster.textScoresGUI[0] = GUIs[1];
        pongMaster.textScoresGUI[1] = GUIs[2];
        pongMaster.paddle2 = paddle;
        pongMaster.gameEndTextGUI = GUIs[3];

        PongMaster.instance.StartGame();
    }
}
