using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler: MonoBehaviour
{
    public int playerScore;
    [SerializeField] public TMP_Text countText;


    private bool GameOver;
    private int pickupTotal;
    private int pickupCount;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        pickupTotal = GameObject.FindGameObjectsWithTag("Pickup").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!AllPickupsCollected())
        {
            countText.text = "Score: " + playerScore.ToString();
        }
        else
        {
            countText.text = "All Pickups Collected! Score: " + playerScore.ToString();
        }
    }

    public void IncreaseScore(int increment)
    {
        playerScore += increment;
        pickupCount++;
    }

    public bool AllPickupsCollected()
    {
        if (pickupCount == pickupTotal)
        {
            return true;
        }
        return false;
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", playerScore);
        int p1 = 0;
        if (AllPickupsCollected()) {
            p1 = 1;
        }
        PlayerPrefs.SetInt("playerWon", p1);
    }

}
