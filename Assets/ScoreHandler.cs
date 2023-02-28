using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int playerScore;
    [SerializeField] public TMP_Text countText;


    private bool GameOver;
    private int pickupTotal;
    private int pickupCount;
    [SerializeField] public GameObject endZone;



    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        pickupTotal = GameObject.FindGameObjectsWithTag("Pickup").Length;
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = "Score: " + playerScore.ToString() + "<br>Pickups Collected: " + pickupCount.ToString() + " / " + pickupTotal.ToString();

    }

    public void IncreaseScore(int increment) {
        playerScore += increment;
        pickupCount++;
    }

    public bool AllPickupsCollected() {
        if (pickupCount == pickupTotal)
        {
            return true;
        }
        return false;
    }
}
