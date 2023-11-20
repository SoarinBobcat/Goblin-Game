using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonerBounce : MonoBehaviour
{
    private CharacterController c_c;

    private Vector3 vel;

    private bool fading = false;
    private float alpha = 1.0f;
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
        if (c_c.isGrounded)
        {
            vel.y = (-vel.y/3)*2;
            fading = true;
        }
        else
        {
            vel.y -= 0.5f;
        }

        if (fading)
        {
            alpha -= 1f * Time.deltaTime;
            this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            if (alpha <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }

        c_c.Move(vel * Time.deltaTime);
    }
}
