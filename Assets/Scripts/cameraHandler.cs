using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    private Vector3 playerLocation;

    private float cameraY;

    [SerializeField] private float cameraSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = player.transform.position; // Get player's coords
        transform.LookAt(playerLocation); // Lock camera to player

        var camZoom = Input.GetAxis("Mouse ScrollWheel");
        camZoom *= -1;
        camZoom *= (cameraSensitivity / 5);

        cam.fieldOfView += camZoom;
        if (cam.fieldOfView < 10) { cam.fieldOfView = 10; }
        if (cam.fieldOfView > 90) { cam.fieldOfView = 90; }


        // Horizontal
        cameraY = Input.GetAxis("Mouse X");
          
        transform.RotateAround(playerLocation, new Vector3(0, cameraY, 0), Time.deltaTime * 5 * cameraSensitivity);

       
    }
}
