using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorID : MonoBehaviour
{
    public GameObject UnlockID;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!UnlockID.activeSelf)
        {
            transform.position += new Vector3(0, -20 * Time.deltaTime, 0);
        }
    }
}
