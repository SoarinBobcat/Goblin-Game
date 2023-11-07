using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("In");
            gameObject.GetComponentInParent<EnemyAI>().PlayerEntered();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Out");
            gameObject.GetComponentInParent<EnemyAI>().PlayerExited();
        }
    }
}
