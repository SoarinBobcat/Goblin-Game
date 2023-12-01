using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBounce : MonoBehaviour
{
    private CharacterController c_c;

    private Vector3 vel;

    private bool following = false;
    private float speed = 0;

    void Start()
    {
        c_c = this.gameObject.GetComponent<CharacterController>();
        vel = new Vector3(Mathf.Sign(Random.Range(-1, 1)) * Random.Range(4, 6), 8, Mathf.Sign(Random.Range(-1, 1)) * Random.Range(4, 6));

        if (Random.Range(1,2) < 2) { speed = 2f; } else { speed = 2f; }

        this.gameObject.GetComponent<Animator>().speed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((c_c.isGrounded) && (!following))
        {
            vel.y = (-vel.y/3)*2;
            following = true;
        }
        else if ((!c_c.isGrounded) && (!following))
        {
            vel.y -= 0.5f;
        }

        if (following)
        {
            //vel = Vector3.zero;
            //transform.position = Vector3.Slerp(transform.position, GameObject.Find("Player").transform.position, 0.1f);
            this.gameObject.SetActive(false);
        }



        c_c.Move(vel * Time.deltaTime);
    }
}
