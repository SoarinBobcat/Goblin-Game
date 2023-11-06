using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;

    public List<Transform> locations = new List<Transform>();
    public Vector3 wander = Vector3.zero;

    public bool aggro = false;
    private float time = 0;

    enum States
    {
        Idle,
        Wander,
        Chase,
        Stun
    }

    States state = States.Idle;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.isStopped = false;

        time = Random.Range(3, 10);
    }

    void Update()
    {
        switch (state)
        {
            case States.Idle:
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    state = States.Wander;
                    wander = WanderDest(transform.position, Random.Range(5f, 10f));
                    agent.SetDestination(wander);
                }
                break;
            case States.Wander:
                if (agent.remainingDistance < 0.2f)
                {
                    time = Random.Range(3, 10);
                    state = States.Idle;
                }
                break;
            case States.Chase:
                agent.SetDestination(player.position);
                if (!aggro)
                {
                    time -= Time.deltaTime;
                }

                if (time <= 0)
                {
                    state = States.Idle;
                }
                break;
            case States.Stun:
                break;
        }
    }

    Vector3 WanderDest(Vector3 center, float range)
    {
        Vector3 temp = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        while (!NavMesh.SamplePosition(temp, out hit, 5.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return hit.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            time = 4;
            aggro = true;
            state = States.Chase;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            aggro = false;
        }
    }
}