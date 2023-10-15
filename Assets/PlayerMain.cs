using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    //Speed Class
    public class MovementSettings
    {
        public float MaxSpd;
        public float Acc;
        public float Decc;

        public MovementSettings(float maxSpd, float acc, float decc)
        {
            MaxSpd = maxSpd;
            Acc = acc;
            Decc = decc;
        }
    }

    //Movement Settings
    [Header("Movement")]
    [SerializeField] private float friction = 6f;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float jumpSpd = 8f;
    [SerializeField] private float hopSpd = 4f;

    [SerializeField] private MovementSettings GroundSettings = new MovementSettings(7,14,10);
    [SerializeField] private MovementSettings AirSettings = new MovementSettings(7,2,2);
    [SerializeField] private MovementSettings StrafeSettings = new MovementSettings(1,50,50);

    private CharacterController player;
    private Transform trans;
    private Vector3 moveDir = Vector3.zero;
    public Vector3 playerVel = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        trans = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (player.isGrounded)
        {
            GroundMove();
        }
        else
        {
            AirMove();
        }

        player.Move(playerVel * Time.deltaTime);
    }

    private void AirMove()
    {
        float accel;

        //Set input to temporary variable and have it relative to 
        var wishDir = moveDir;
        wishDir = trans.TransformDirection(wishDir);

        //Sets whether the player wants to move or not
        float wishSpd = wishDir.magnitude;
        wishSpd *= AirSettings.MaxSpd;

        //Determines whether acc or decc value should be used dependent on input
        float wishSpd2 = wishSpd;
        if (Vector3.Dot(playerVel, wishDir) < 0)
        {
            accel = AirSettings.Decc;
        }
        else
        {
            accel = AirSettings.Acc;
        }

        // If the player is ONLY strafing left or right
        if (moveDir.z == 0 && moveDir.x != 0)
        {
            if (wishSpd > StrafeSettings.MaxSpd)
            {
                wishSpd = StrafeSettings.MaxSpd;
            }

            accel = StrafeSettings.Acc;
        }

        Accelerate(wishDir, wishSpd, accel);
        AirControl(wishDir, wishSpd2);

        playerVel.y -= gravity;
    }

    private void GroundMove()
    {
        playerVel.y = -gravity * Time.deltaTime;

        float accel;

        //Set input to temporary variable and have it relative to 
        var wishDir = moveDir;
        wishDir = trans.TransformDirection(wishDir);

        //Sets whether the player wants to move or not
        float wishSpd = wishDir.magnitude;
        wishSpd *= AirSettings.MaxSpd;

        if (Input.GetButton("Jump"))
        {
            ApplyFriction(0);
            playerVel.y = jumpSpd;
        }
        else
        {
            ApplyFriction(1f);
        }

        Accelerate(wishDir, wishSpd, GroundSettings.Acc);
    }

    private void Accelerate(Vector3 targetDir, float targetSpd, float accel)
    {
        float currSpd = Vector3.Dot(playerVel, targetDir);
        float addSpd = targetSpd - currSpd;
        if (addSpd <= 0)
        {
            return;
        }

        float accSpd = accel * Time.deltaTime * targetSpd;
        if (accSpd > addSpd)
        {
            accSpd = addSpd;
        }

        playerVel.x += accSpd * targetDir.x;
        playerVel.z += accSpd * targetDir.z;
    }

    private void AirControl(Vector3 targetDir, float targetSpd)
    {
        // Only control air movement when moving forward or backward.
        if (Mathf.Abs(moveDir.z) < 0.001 || Mathf.Abs(targetSpd) < 0.001)
        {
            return;
        }

        float ySpd = playerVel.y;

        float spd = playerVel.magnitude;
        playerVel.Normalize();

        float dot = Vector3.Dot(playerVel, targetDir);
        float k = 32;
        k *= 0.2f * dot * dot * Time.deltaTime;

        // Change direction while slowing down.
        if (dot > 0)
        {
            playerVel.x *= spd + targetDir.x * k;
            playerVel.y *= spd + targetDir.y * k;
            playerVel.z *= spd + targetDir.z * k;

            playerVel.Normalize();
        }

        playerVel.x *= spd;
        playerVel.y = ySpd;
        playerVel.z *= spd;
    }

    private void ApplyFriction(float t)
    {
        Vector3 vec = playerVel;
        vec.y = 0;
        float spd = vec.magnitude;
        float drop = 0;

        float control = spd < GroundSettings.Decc ? GroundSettings.Decc : spd;
        drop = control * friction * Time.deltaTime * t;

        float newSpd = spd - drop;
        if (newSpd < 0)
        {
            newSpd = 0;
        }

        if (spd > 0)
        {
            newSpd /= spd;
        }

        playerVel.x *= newSpd;
        playerVel.z *= newSpd;
    }
}
