using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int playerScore;
    [SerializeField] public TMP_Text countText;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = "Score: " + playerScore.ToString();
    }

    public void IncreaseScore(int increment) {
        playerScore += increment;
    }

}
