using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button mQuitBtn;

    // Start is called before the first frame update
    void Start()
    {
        // Add a callback to the button, when the button is clicked the PlayGame() function will be called on the game manager.
        startBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.PlayGame();
        });

        // Add a callback to the button, when the button is clicked the QuitGame() function will be called on the game manager.
        mQuitBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.QuitGame();
        });
    }
}
