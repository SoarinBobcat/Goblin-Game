using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject treasureOpen;

    //Really hacky way to not do more work :/
    //Switches treasure for fake treasure
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hurtbox")
        {
            treasureOpen.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
