using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorID : MonoBehaviour
{
    public List<GameObject> UnlockID = new List<GameObject>();

    // Update is called once per frame
    void FixedUpdate()
    {
        if (KeyCheck())
        {
            transform.position += new Vector3(0, -20 * Time.deltaTime, 0);
        }
    }

    bool KeyCheck()
    {
        bool unlocked = true;
        foreach (GameObject key in UnlockID)
        {
            if (key.activeSelf)
            {
                unlocked = false;
                break;
            }
        }

        return unlocked;
    }
}
