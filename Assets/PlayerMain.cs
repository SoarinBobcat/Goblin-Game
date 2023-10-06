using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    //Speed Class
    public class MovementSettings
    {
        public float MaxSpeed;
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
    [SerializeField] private MovementSettings groundSettings = new MovementSettings(7,14,10);
    [SerializeField] private MovementSettings airSettings = new MovementSettings(7,2,2);
    [SerializeField] private MovementSettings strafeSettings = new MovementSettings(1,50,50);

    private CharacterController player;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 playerVel = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Fixed_Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Horizontal"));

        if (player.isGrounded)
        {
            GroundMove();
        }
        else
        {
            AirMove();
        }

        player.Move(player.Vel * Time.deltaTime);
    }

    private void AirMove()
    {
        //nothing :(
    }

    private void GroundMove()
    {
        //nothing :(
    }
}
