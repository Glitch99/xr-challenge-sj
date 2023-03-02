using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText) {
            int playerWon = 1;//PlayerPrefs.GetInt("playerWon");
            int playerScore = PlayerPrefs.GetInt("score");
            if (playerWon == 0)
            {
                scoreText.text = "Score: " + playerScore.ToString();
            }
            else { 
                scoreText.text = "You Found All The Stars!<br>Final Score: " + playerScore.ToString();
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame() {
        SceneManager.LoadScene(sceneName: "IslandLevel");
    }

    public void LoadMenu() {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(sceneName: "GameOverScreen");
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene(sceneName: "GameWinScene");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(sceneName: "CreditsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
