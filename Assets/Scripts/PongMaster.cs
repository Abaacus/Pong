using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum Difficulty { easy, medium, hard, ultimate };

public class PongMaster : MonoBehaviour
{
    public static PongMaster instance;
    public bool twoPlayer;
    public GameObject paddle2;

    public Difficulty difficulty;

    public int[] intScores = new int[2];
    public TextMeshProUGUI[] textScoresGUI = new TextMeshProUGUI[2];
    string[] textScores = new string[2];

    public TextMeshProUGUI timerGUI;
    string timer;
    public float totalGameTime = 20f;
    float timeElapsed;

    public TextMeshProUGUI gameEndTextGUI;
    string gameEndText;

    public bool gameStarted;
    public bool gameEnded;
    public bool loadSettings;

    public KeyCode[] selfDestructSequence;
    List<KeyCode> keysPressed = new List<KeyCode>();
    bool selfDestruct;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple instances of PongMaster found");
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this);

        StartCoroutine("WaitToKillGame");
    }

    public void LaunchGame()
    {
        loadSettings = false;
        PrintSetings();
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPlayers(bool players)
    {
        twoPlayer = players;
    }

    public void SetDifficulty(int newValue)
    {
        switch (newValue)
        {
            default:
                difficulty = Difficulty.medium;
                break;

            case 1:
                difficulty = Difficulty.easy;
                break;

            case 2:
                difficulty = Difficulty.medium;
                break;

            case 3:
                difficulty = Difficulty.hard;
                break;

            case 4:
                difficulty = Difficulty.ultimate;
                break;
        }
    }

    void PrintSetings()
    {
        if (twoPlayer)
        {
            Debug.Log("Two Player");
        }
        else
        {
            Debug.Log("One Player");
        }

        Debug.Log("Difficulty:" + difficulty);
    }

    public void StartGame()
    {
        gameEndTextGUI.enabled = false;

        if (twoPlayer)
        {
            paddle2.GetComponent<Paddle>().enabled = false;
            paddle2.GetComponent<PaddleAI>().enabled = true;
        }
        else
        {
            paddle2.GetComponent<Paddle>().enabled = true;
            paddle2.GetComponent<PaddleAI>().enabled = false;
        }

        timerGUI = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();

        timeElapsed = 0f;
        Debug.Log("Difficulty: " + difficulty);

        UpdateScores();
        UpdateTime();

        gameStarted = true;
    }

    void Update()
    {
        if (gameStarted)
        {
            timeElapsed += Time.deltaTime;
            UpdateTime();
        }

        if (gameEnded)
        {
            EndGame();
        }

        BreakGame();
    }

    void EndGame()
    {
        if (intScores[0] > intScores[1])
        {
            gameEndText = "Player 1 Wins!";
        }
        if (intScores[0] < intScores[1])
        {
            gameEndText = "Player 2 Wins!";
        }
        if (intScores[0] == intScores[1])
        {
            gameEndText = "It's a tie!";
        }

        gameEndTextGUI.text = gameEndText;
        gameEndTextGUI.enabled = true;

        StartCoroutine("WaitToKillGame");
    }

    IEnumerator WaitToKillGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
        gameEnded = false;
        loadSettings = true;
        StopCoroutine("WaitToKillGame");
    }

    void UpdateTime()
    {
        int timeRemaining = Mathf.RoundToInt(totalGameTime - timeElapsed);

        timer = TimeSpan.FromSeconds(timeRemaining).ToString(@"mm\:ss");
        timerGUI.text = timer;

        if (timeElapsed >= totalGameTime)
        {
            gameStarted = false;
            gameEnded = true;
        }
    }

    public void UpdateScores()
    {
        Debug.Log("Updating scores");
        
        textScores[0] = "P1: " + intScores[0];
        textScores[1] = "P2: " + intScores[1];

        textScoresGUI[0].text = textScores[0];
        textScoresGUI[1].text = textScores[1];
    }

    void BreakGame()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                keysPressed.Add(key);
            }
        }

        if (keysPressed != null)
        {
            selfDestruct = true;
            if (keysPressed.Count <= selfDestructSequence.Length)
            {
                for (int i = 0; i < keysPressed.Count; i++)
                {
                    if (keysPressed[i] != selfDestructSequence[i])
                    {
                        selfDestruct = false;
                        keysPressed.Clear();
                        break;
                    }
                }
            }
            else
            {
                selfDestruct = false;
                keysPressed.Clear();
            }


            if (keysPressed.Count == selfDestructSequence.Length)
            {
                if (selfDestruct)
                {
                    Debug.Log("Self Destruct activated");
                    Destroy(this);
                }
            }
        }
    }
}
