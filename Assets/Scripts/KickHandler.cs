using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickHandler : MonoBehaviour
{
    public GameObject KickBox;

    void KickOn()
    {
        KickBox.SetActive(true);
    }

    void KickOff()
    {
        KickBox.SetActive(false);
    }
}
