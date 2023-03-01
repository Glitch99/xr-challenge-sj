using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private CharacterController playerController;
    private Vector3 playerVelocity;
    private bool onGround;


    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float playerJump = 3.0f;
    [SerializeField] private float playerGravity = 9.8f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject scoreHandler;

    [SerializeField] private float sprintMult = 1;

    private Vector3 playerMovement;

    private void Start()
    {
        playerController = gameObject.AddComponent<CharacterController>(); // add character controller component
        playerGravity *= -1;
        onGround = false;
    }

    void Update()
    {
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

        if (Input.GetButtonDown("Jump") && onGround) // if jump pressed and not already jumping
        {
            playerVelocity.y += Mathf.Sqrt(playerJump * -3.0f * playerGravity); // increase velocity by jump height
        }

        if (Input.GetButton("Exit"))
        {
            endGame();
        }

    }

    void FixedUpdate()
    {
        // MOVEMENT
        onGround = playerController.isGrounded;
        if (onGround && playerVelocity.y < 0)
        {
            playerVelocity.y = 0.0f;
        }

        var angledMovement = cameraTransform.rotation * playerMovement; // multiply by camera rotation

        playerController.Move(angledMovement * Time.deltaTime * (playerSpeed * sprintMult)); // move player horizontally

        playerVelocity.y += (playerGravity * Time.deltaTime); // move player downwards for gravity

        playerController.Move(playerVelocity * Time.deltaTime); // move player vertically
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            pickupStar(other.gameObject);
        }
        if (other.gameObject.CompareTag("EndZone"))
        {
            if (scoreHandler.GetComponent<ScoreHandler>().AllPickupsCollected())
            {
                endGame();
            }
        }
    }

    private void pickupStar(GameObject star)
    {
        scoreHandler.GetComponent<ScoreHandler>().IncreaseScore(star.GetComponent<Pickup>().GetPickedUp());
        star.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void endGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }

}