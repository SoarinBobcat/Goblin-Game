using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onaroll : MonoBehaviour
{
    public bool move = false;
    public bool stop = false;
    private Vector3 spd;

    public CharacterController c_c;
    public GameObject trail;

    public int max = 10;
    public int min = 1;

    // Start is called before the first frame update
    void Start()
    {
        spd = new Vector3(Random.Range(min, max), 0, 0);
        trail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (!stop)
            {
                c_c.Move(spd * Time.deltaTime);
            }
            else
            {
                stop = !stop;
                yield trail.SetActive(false);
            }
        }
    }

    public void MoveToggle()
    {
        var temp = Random.Range(min, max);
        if ((temp >= max-1) && (max > 4))
        {
            max -= 2;
            min = 1;
            stop = true;
            trail.SetActive(true);
        }
        if ((temp <= min+2) && (min < 4))
        {
            min += 1;
            temp += 2;
            max = 10;
        }
        move = !move;
        spd = new Vector3(temp, 0, 0);
    }
}
