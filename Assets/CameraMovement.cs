using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float cameraSensitivity = 1.5f;
    float cameraVerticalRotation = 0f;

    public Image scythe;
    private float scytheX;
    private float scytheY;

    private GameObject playerObj;
    private float playerVel = 0;

    [SerializeField] private float XoffsetMax = 30f;
    [SerializeField] private float YoffsetMax = 20f;

    [SerializeField] private float timerRate = 0.1f;
    private float timer = 0f;
    private bool right = true;

    private float ScreenWidthDefault = 1920;
    private float ScreenHeightDefault = 1080;

    private float ratioWidth;
    private float ratioHeight;

    // Start is called before the first frame update
    void Start()
    {
        ratioWidth = Screen.width / ScreenWidthDefault;
        ratioHeight = Screen.height / ScreenHeightDefault;

        Cursor.lockState = CursorLockMode.Locked;
        scytheX = scythe.transform.position.x;
        scytheY = scythe.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        ratioWidth = Screen.width / ScreenWidthDefault;
        ratioHeight = Screen.height / ScreenHeightDefault;

        float camX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float camY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        cameraVerticalRotation -= camY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -80f, 80f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * camX);

        //UI Schtuff
        playerVel = player.GetComponent<PlayerMain>().playerVel.magnitude;
        if (playerVel > 0.2f)
        {
            //add onto walk timer
            if (right)
            {
                timer += timerRate * Time.deltaTime;
            }
            else
            {
                timer -= timerRate * Time.deltaTime;
            }

            if (Mathf.Abs(timer) > 1)
            {
                right = !right;
            }
        }
        else
        {
            timer = 0f;
            right = true;
        }

        //calculate x and y offset using walk timer and add them as an offset
        scythe.transform.position = new Vector2((scytheX + (timer * XoffsetMax)), (scytheY + (Mathf.Abs(timer) * YoffsetMax)));
    }
}
