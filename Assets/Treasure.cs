using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject treasureOpen;
    public GameObject coin;

    //Really hacky way to not do more work :/
    //Switches treasure for fake treasure
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hurtbox")
        {
            treasureOpen.SetActive(true);
            for (var i = 0; i < 6; i++)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            this.gameObject.SetActive(false);
        }
    }
}
