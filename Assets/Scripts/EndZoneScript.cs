using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneScript : MonoBehaviour
{
    [SerializeField] private GameObject scoreHandler;

    [SerializeField] private float spinSpeed = .2f;
    [SerializeField] private GameObject blockCircle;
    [SerializeField] private GameObject enterCircle;
    [SerializeField] private GameObject endFire;

    private bool EndGameTriggered;


    // Start is called before the first frame update
    void Start()
    {
        EndGameTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreHandler.GetComponent<ScoreHandler>().AllPickupsCollected())
        {
            if (!EndGameTriggered) {
                EndGameTriggered = true;
                blockCircle.SetActive(false);
                enterCircle.SetActive(true);
                endFire.SetActive(true);
            }
            // Rotate the object around Z
            enterCircle.transform.Rotate(new Vector3(0, spinSpeed, 0), Space.Self);
        }
    }
}
