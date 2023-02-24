using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerLocation;

    private float cameraX;
    private float cameraY;
    private float cameraZ;

    [SerializeField] private float cameraSensitivity = 2;

    private Vector3 cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = player.transform.position; // Get player's coords
        transform.LookAt(playerLocation); // Lock camera to player

        if (Input.GetMouseButton(1))
        {
            // X
            cameraX = Input.GetAxis("Mouse Y");
            transform.RotateAround(playerLocation, new Vector3(cameraX, 0, 0), Time.deltaTime * 5 * cameraSensitivity);


            // Y
            cameraY = Input.GetAxis("Mouse X");
            cameraY *= -1; // invert camera
            transform.RotateAround(playerLocation, new Vector3(0, cameraY * 0.1f, 0), Time.deltaTime * 5 * cameraSensitivity);
           
        }
    }
}
