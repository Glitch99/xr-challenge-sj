using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private CharacterController playerController;
    private Vector3 playerVelocity;
    private bool playerGrounded;


    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float playerJump = 3.0f;
    [SerializeField] private float playerGravity = 9.81f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject scoreHandler;

    [SerializeField] private float sprintMult = 1;

    private void Start()
    {
        playerController = gameObject.AddComponent<CharacterController>(); // add character controller component

    }

    void Update()
    {
        playerGrounded = playerController.isGrounded; // check if player is on ground
        if (playerGrounded && playerVelocity.y < 0) // if player is on ground and not moving vertically already
        {
            playerVelocity.y = 0f; // set player vertical velocity to zero
        }

        // HORIZONTAL MOVEMENT
        Vector3 playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // get left/right and up/down from input, add to vector

        var angledMovement = cameraTransform.rotation * playerMovement; // multiply by camera rotation
        angledMovement.y = 0; // set vertical movement from camera to 0


        // SPRINT
        if (Input.GetButton("Sprint") && playerGrounded) // if shift pressed and on ground
        {
            sprintMult = 1.5f; // increase player speed
        }
        else {
            sprintMult = 1;
        }

        playerController.Move(angledMovement * Time.deltaTime * (playerSpeed * sprintMult)); // move player horizontally

        // JUMPING
        if (Input.GetButtonDown("Jump") && playerGrounded) // if jump pressed and on ground
        {
            playerVelocity.y += Mathf.Sqrt(playerJump * playerGravity); // increase velocity by square root of jump value times gravity
        }

        playerVelocity.y -= playerGravity * Time.deltaTime; // move player downwards for gravity
        playerController.Move(playerVelocity * Time.deltaTime); // move player controller
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pickup")) {
            scoreHandler.GetComponent<ScoreHandler>().IncreaseScore(other.gameObject.GetComponent<Pickup>().GetPickedUp());
        }
    }
}
