using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private CharacterController playerController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject scoreHandler;

    [SerializeField] private float sprintMult = 1;
    [SerializeField] private float playerSpeed = 3.0f;
    private Vector3 playerMovement;

    private Vector3 playerVelocity;
    [SerializeField] private float playerJump = 3.0f;
    [SerializeField] private float playerGravity = 9.8f;
    private bool onGround;

    private bool isCrouching;
    private float normalHeight;

    [SerializeField] private GameObject audioHandler;
    [SerializeField] private GameObject menuHandler;

    private void Start()
    {
        playerController = gameObject.AddComponent<CharacterController>(); // add character controller component
        playerGravity *= -1;
        onGround = false;
        isCrouching = false;
        normalHeight = playerController.height;
    }

    void Update()
    {
        // Get Input
        playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // get left/right and up/down from input, add to vector


        if (Input.GetButtonDown("Jump") && onGround) // if jump pressed and not already jumping
        {
            audioHandler.GetComponent<AudioHandler>().playSoundEffect(0);
            playerVelocity.y += Mathf.Sqrt(playerJump * -3.0f * playerGravity); // increase velocity by jump height
        }

        if (Input.GetButtonDown("Crouch") && isCrouching == false)
        {
            audioHandler.GetComponent<AudioHandler>().playSoundEffect(1);
        }
        if (Input.GetButton("Crouch"))
        {
            isCrouching = true;
        }
        else {
            isCrouching = false;
        }
 
        if (Input.GetButton("Sprint") && !isCrouching) // if shift pressed
        {
            sprintMult = 1.5f; // increase player speed
        }
        else
        {
            sprintMult = 1;
        }

        if (Input.GetButtonDown("Exit"))
        {
            menuHandler.GetComponent<MenuHandler>().ExitGame();
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

        if (isCrouching)
        {
            playerMovement *= .5f;
            playerController.height = normalHeight * .3f;
            transform.localScale = new Vector3(.5f, .25f, .5f);
        }
        else {
            playerController.height = normalHeight;
            transform.localScale = new Vector3(.5f, .5f, .5f);
        }

        var angledMovement = cameraTransform.rotation * playerMovement; // multiply by camera rotation
        angledMovement.y = 0;
        playerController.Move(angledMovement * Time.deltaTime * (playerSpeed * sprintMult)); // move player horizontally

        playerVelocity.y += (playerGravity * Time.deltaTime); // move player downwards for gravity

        playerController.Move(playerVelocity * Time.deltaTime); // move player vertically

        
        if (transform.position.y <= -10)
        {
            menuHandler.GetComponent<MenuHandler>().LoadGameOver();
        }

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
                menuHandler.GetComponent<MenuHandler>().LoadWinScreen();
            }
        }
        if (other.gameObject.CompareTag("Ocean"))
        {
            audioHandler.GetComponent<AudioHandler>().playSoundEffect(6);
        }
    }

    private void pickupStar(GameObject star)
    {
        scoreHandler.GetComponent<ScoreHandler>().IncreaseScore(star.GetComponent<Pickup>().GetPickedUp());
        star.GetComponent<CapsuleCollider>().enabled = false;
        star.GetComponent<Light>().enabled = false;
        audioHandler.GetComponent<AudioHandler>().playSoundEffect(2);

        if (scoreHandler.GetComponent<ScoreHandler>().AllPickupsCollected())
        {
            audioHandler.GetComponent<AudioHandler>().SwitchMood();
        }
    }
}