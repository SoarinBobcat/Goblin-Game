using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyID : MonoBehaviour
{
    //Disappear upon touching player
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
