using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private CharacterController C_C;

    public Transform player;

    public List<Transform> locations = new List<Transform>();
    public Vector3 wander = Vector3.zero;

    private int HP = 999;
    public bool aggro = false;
    private float time = 0;

    private float gravity = 0.5f;
    public Vector3 enemyVel = Vector3.zero;

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
        C_C = this.gameObject.GetComponent<CharacterController>();

        agent.updatePosition = false;
        agent.updateRotation = false;

        time = Random.Range(3, 10);
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case States.Idle:
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
                    time = Random.Range(1, 4);
                    state = States.Idle;
                }
                break;
            case States.Chase:
                agent.SetDestination(player.position);

                if (time <= 0)
                {
                    state = States.Idle;
                }
                break;
            case States.Stun:
                break;
        }

        if (HP <= 0)
        {
            this.gameObject.SetActive(false);
        }

        if (C_C.isGrounded)
        {

            enemyVel = Vector3.zero;
            C_C.Move(agent.velocity*2 * Time.deltaTime);
        }
        else
        {
            enemyVel.y -= gravity;
            C_C.Move(enemyVel * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        if (!aggro)
        {
            time -= Time.deltaTime;
        }

        /*Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        if (worldDeltaPosition.magnitude > agent.radius)
        {*/
        agent.nextPosition = transform.position; //+ 0.1f * worldDeltaPosition;
        //}
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

    public void PlayerEntered()
    {
        time = 4;
        aggro = true;
        state = States.Chase;
    }

    public void PlayerExited()
    {
        aggro = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hurtbox")
        {
            Debug.Log("Hit!");

            HP -= other.GetComponentInParent<HurtboxInfo>().damage;
            enemyVel = Vector3.Normalize(transform.position-other.GetComponentInParent<Transform>().position)* other.GetComponentInParent<HurtboxInfo>().knockback;
            enemyVel.y = 4;
        }
    }
}