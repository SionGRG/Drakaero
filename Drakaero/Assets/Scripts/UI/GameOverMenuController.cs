using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button quitBtn;

    private void Start()
    {
        // Add a callback to the button, when the button is clicked the PlayGame() function will be called on the game manager.
        restartBtn.onClick.AddListener(() => 
        {
            GameManager.Instance.PlayGame();
        });

        // Add a callback to the button, when the button is clicked the QuitGame() function will be called on the game manager.
        quitBtn.onClick.AddListener(() => 
        {
            GameManager.Instance.QuitGame();
        });
    }
}
