using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private CharacterController playerController;
    //private float playerVelocity;
    private bool playerGrounded;


    [SerializeField] private float playerSpeed = 3.0f;
    //[SerializeField] private float playerJump = 3.0f;
    // [SerializeField] private float playerGravity;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject scoreHandler;

    [SerializeField] private float sprintMult = 1;

    private Vector3 playerMovement;

    private void Start()
    {
        playerController = gameObject.AddComponent<CharacterController>(); // add character controller component
    }

    void Update() {
        // Get Input
        playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // get left/right and up/down from input, add to vector


        if (Input.GetButton("Sprint")) // if shift pressed
        {
            sprintMult = 1.5f; // increase player speed
        }
        else
        {
            sprintMult = 1;
        }
    }

    void FixedUpdate()
    {
        // HORIZONTAL MOVEMENT
      

        var angledMovement = cameraTransform.rotation * playerMovement; // multiply by camera rotation
        angledMovement.y = -5.0f;

        

        playerController.Move(angledMovement * Time.deltaTime * (playerSpeed * sprintMult)); // move player horizontally

        // JUMPING (IMPLEMENT IF TIME ALLOWS)

        /*  playerGrounded = playerController.isGrounded; // check if player is on ground
        Debug.Log("onGround? " + playerController.isGrounded);

        if (Input.GetButtonDown("Jump") && playerGrounded) // if jump pressed and on ground
        {
            Debug.Log("Jump?");
            playerVelocity.y += Mathf.Sqrt(playerJump * -2 * playerGravity); // increase velocity by jump height
        }

        if (playerGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0.0f; 
        }

        playerVelocity.y += (playerGravity * Time.deltaTime); // move player downwards for gravity


        playerController.Move(playerVelocity * Time.deltaTime); // move player vertically


        */
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            pickupStar(other.gameObject);
        }
        else if (other.gameObject.CompareTag("EndZone")) {
            if (scoreHandler.GetComponent<ScoreHandler>().AllPickupsCollected()) { 
                endGame();
            }
        }
    }

    private void pickupStar(GameObject star) {
        scoreHandler.GetComponent<ScoreHandler>().IncreaseScore(star.GetComponent<Pickup>().GetPickedUp());
        star.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void endGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }

}