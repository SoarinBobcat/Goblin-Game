using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private NavMeshAgent wanderNode;

    public List<Transform> locations = new List<Transform>();

    public bool moving = false;
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
        if (!moving)
        {
            moving = !moving;
            Vector3 temp = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            wanderNode.Move(temp);
        }

        agent.SetDestination(wanderNode.transform.position);
    }
}
