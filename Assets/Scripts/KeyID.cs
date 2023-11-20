using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyID : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
