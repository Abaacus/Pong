using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAssigner : MonoBehaviour
{
    public Button button;
    public enum ButtonType { launchGame, setPlayerType1, setPlayerType2, quitGame};
    public ButtonType[] buttonTypes;
    PongMaster pongMaster;

    void Start()
    {
        StartCoroutine("InitializeButtons");
    }

    IEnumerator InitializeButtons()
    {
        pongMaster = PongMaster.instance;
        yield return new WaitUntil(() => pongMaster.loadSettings);

        foreach (ButtonType buttonType in buttonTypes)
        {
            switch (buttonType)
            {
                default:
                    Debug.Log("Invalid ButtonType");
                    break;

                case ButtonType.launchGame:
                    Debug.Log("Adding LaunchGame()");
                    button.onClick.AddListener(pongMaster.LaunchGame);
                    break;

                case ButtonType.setPlayerType1:
                    Debug.Log("Adding SetPlayers(true)");
                    button.onClick.AddListener(() => pongMaster.SetPlayers(true));
                    break;

                case ButtonType.setPlayerType2:
                    Debug.Log("Adding SetPlayers(false)");
                    button.onClick.AddListener(() => pongMaster.SetPlayers(false));
                    break;

                case ButtonType.quitGame:
                    Debug.Log("Adding QuitGame()");
                    button.onClick.AddListener(pongMaster.QuitGame);
                    break;
            }
        }

        StopCoroutine("InitializeButtons");
    }
}
