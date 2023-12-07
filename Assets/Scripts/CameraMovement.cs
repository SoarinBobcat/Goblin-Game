using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float cameraSensitivity = 1.5f;
    float cameraVerticalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float camX = (Input.GetAxis("Mouse X") + (Input.GetAxis("Look Horizontal"))) * cameraSensitivity * Time.deltaTime;
            float camY = (Input.GetAxis("Mouse Y") + (Input.GetAxis("Look Vertical"))) * cameraSensitivity * Time.deltaTime;


            cameraVerticalRotation -= camY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -80f, 80f);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

            player.Rotate(Vector3.up * camX);
        }
    }
}
