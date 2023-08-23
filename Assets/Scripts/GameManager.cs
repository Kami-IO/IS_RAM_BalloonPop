using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Game States Enum
public enum GameState
{
    SignUp,
    Login,
    GameStart,
    Playing,
    Paused,
    GameEnd,
}

// Game Manager
public class GameManager : MonoBehaviour
{
    // Creates an instance of Game Manager
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    // GameState Variables
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;


    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Timer time;
    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private Points points;

    GameObject LoginScreen;
    GameObject EndScreen;

    static int _higScore;

    void Awake()
    {
        // Define instance reference & make it indestructable
        _instance = this;
        DontDestroyOnLoad(this.gameObject); 
    }

    void Update()
    {
        if(time.timeIsRunning == false)
        {
            
        }
    }


    void Start()
    {

        //Game Initial State
        UpdateGameState(GameState.Login);

    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SignUp:
                HandleLogin();
                break;

            case GameState.Login:
                HandleLogin();
                break;

            case GameState.GameStart:
                HandleStartRound();
                break;

            case GameState.Playing:
                HandlePlayingGame();
                break;

            case GameState.Paused:
                HandlePauseGame();
                break;

            case GameState.GameEnd:
                HandleEndGame();
                break;

            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }


    public void HandleLogin()
    {
        LoginScreen = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Authentication"), canvas.transform);
    }

    public void HandleStartRound()
    {
        Destroy(LoginScreen);
        spawner.gameObject.SetActive(true);
        time.timeIsRunning = true;
    }

    public void HandlePlayingGame()
    {
        Time.timeScale = 1f;
    }

    public void HandlePauseGame()
    {
        Time.timeScale = 0f;
    }

    public void HandleEndGame()
    {
        spawner.gameObject.SetActive(false);
        EndScreen = Instantiate(Resources.Load<GameObject>("Prefabs/UI/EndScreen"), canvas.transform);
    }

    
}


