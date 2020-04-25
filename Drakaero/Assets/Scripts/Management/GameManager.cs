using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState { START, GO2START, IN_GAME, GAME_OVER };
    private GameState gameState;
    [SerializeField] private GameState startState = GameState.START; // Exists to enable individual level testing

    [SerializeField] private GameObject gameOverPrefab;

    static private GameManager instance = null;

    // Lets other scripts find the instance of the game manager
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Ensure there is only one instance of this object in the game
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("Theme");
        gameState = startState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnChangeState(GameState newState)
    {
        if (gameState != newState)
        {
            switch (newState)
            {
                case GameState.START:
                    break;

                case GameState.GO2START:
                    SceneManager.LoadScene("Start");                // Load the 'Start' scene
                    break;

                case GameState.IN_GAME:

                    Time.timeScale = 1;                             // Set timescale to be a normal rate 
                    SceneManager.LoadScene("Drakaero");             // Load the 'Drakaero' scene
                    Cursor.lockState = CursorLockMode.Locked;       // Lock the cursor to the game screen
                    Cursor.visible = false;                         // Hide mouse cursor        
                    break;

                case GameState.GAME_OVER:

                    Cursor.lockState = CursorLockMode.None;         // unlock the cursor for the menu
                    Cursor.visible = true;                          // Show mouse cursor
                    EnableInput(false);                             // disable character controls
                    Time.timeScale = 0;                             // Pause the game by setting timescale to 0 to stop AI behaviour
                    Instantiate(gameOverPrefab);                    // Instantiate the GameOver menu prefab

                    break;
            }

            gameState = newState;
        }
    }
    private void EnableInput(bool input)
    {
        // Find the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerControls>().enabled = input;
        //player.GetComponentInChildren<Weapon>().enabled = input; // If we decide to add a weapon for unnamed

    }
    public void PlayGame()
    {
        OnChangeState(GameState.IN_GAME);
    }

    public void GoToStartMenu()
    {
        OnChangeState(GameState.GO2START);
    }

    public void GameOver()
    {
        OnChangeState(GameState.GAME_OVER);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
