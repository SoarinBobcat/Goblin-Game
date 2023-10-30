using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    public List<Transform> locations = new List<Transform>();

    public bool moving = true;
    private Vector3 currLocale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        //currLocale = locations[Random.Range(0, ((locations.Count) - 1))].position;
        //agent.SetDestination(currLocale);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector3.Distance(transform.position, currLocale) < 1f)
        {
            if (!moving)
            {
                var temp = locations[Random.Range(0, ((locations.Count) - 1))].position;
                while (currLocale == temp)
                {
                    temp = locations[Random.Range(0, ((locations.Count) - 1))].position;
                }
                currLocale = temp;

                agent.SetDestination(currLocale);
                moving = !moving;
            }
            moving = !moving;
        }*/
        agent.Move(new Vector3(1 * Time.deltaTime, 0, 0));
    }
}
