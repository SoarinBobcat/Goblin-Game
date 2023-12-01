using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private CharacterController C_C;

    public Transform player;
    public GameObject stunParticle;
    public GameObject hitBox;

    public List<Transform> locations = new List<Transform>();
    public Vector3 wander = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;

    private int HP = 8;
    public bool aggro = false;
    private float time = 0;

    private float gravity = 0.5f;
    public Vector3 enemyVel = Vector3.zero;

    public GameObject boner;
    public Animator anim;

    enum States
    {
        Idle,
        Wander,
        Chase,
        AttackPrep,
        Attack,
        Stun
    }

    States state = States.Idle;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        C_C = this.gameObject.GetComponent<CharacterController>();

        lastPos = transform.position;

        time = Random.Range(1f, 4f);
    }

    void Update()
    {
        if ((!aggro) || (state == States.Stun))
        {
            time -= Time.deltaTime;
        }

        if ((aggro) && ((state == States.AttackPrep) || (state == States.Idle)))
        {
            time -= Time.deltaTime;
        }

        agent.nextPosition = transform.position;

        switch (state)
        {
            case States.Idle:
                anim.SetInteger("State", 0);
                if (time <= 0)
                {
                    if (aggro)
                    {
                        state = States.Chase;
                        agent.SetDestination(player.position);
                    }
                    else
                    {
                        state = States.Wander;
                        wander = WanderDest(transform.position, Random.Range(10f, 20f));
                        agent.SetDestination(wander);
                    }
                }

                break;
            case States.Wander:
                anim.SetInteger("State", 1);
                if (agent.remainingDistance < 0.2f)
                {
                    time = Random.Range(1f, 4f);
                    state = States.Idle;
                }
                break;
            case States.Chase:
                anim.SetInteger("State", 2);
                agent.SetDestination(player.position);

                if (!aggro)
                {
                    if (time <= 0)
                    {
                        state = States.Idle;
                    }
                }
                else
                {
                    time = 4f;
                }

                if (Vector3.Distance(transform.position, player.position) < 8f)
                {
                    time = 0.5f;
                    state = States.AttackPrep;
                }

                break;
            case States.AttackPrep:
                anim.SetInteger("State", 3);
                agent.SetDestination(transform.position);
                if (time <= 0)
                {
                    enemyVel = Vector3.Normalize(player.position - transform.position) * 20;
                    enemyVel.y = 8f;
                    C_C.Move(Vector3.up * 0.1f);
                    Vector3 pPos = new Vector3(player.position.x, 0, player.position.z);
                    transform.LookAt(player.position);
                    state = States.Attack;
                }
                break;
            case States.Attack:
                anim.SetInteger("State", 4);
                hitBox.SetActive(true);

                if (C_C.isGrounded)
                {
                    aggro = true;
                    time = 1f;
                    state = States.Idle;
                    agent.SetDestination(transform.position);
                    hitBox.SetActive(false);
                }
                break;
            case States.Stun:
                anim.SetInteger("State", 5);
                stunParticle.SetActive(true);
                hitBox.SetActive(false);
                if (time <= 0)
                {
                    stunParticle.SetActive(false);

                    if (aggro)
                    {
                        state = States.Chase;
                        agent.SetDestination(player.position);
                    }
                    else
                    {
                        time = Random.Range(1f, 4f);
                        state = States.Idle;
                        agent.SetDestination(transform.position);
                    }

                    Debug.Log(state);
                }
                break;

                
        }

        if (HP <= 0)
        {
            for (var i = 0; i < 6; i++)
            {
                Instantiate(boner, transform.position, Quaternion.identity);
            }
            this.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        EnemyMove();
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

    void EnemyMove()
    {
        if (C_C.isGrounded)
        {
            enemyVel = Vector3.zero;
            if (state != States.Stun)
            {
                agent.updatePosition = true;
                agent.updateRotation = true;
            }
        }
        else
        {

            agent.updatePosition = false;
            agent.updateRotation = false;

            enemyVel.y -= gravity;

            lastPos = transform.position;
            C_C.Move(enemyVel * Time.deltaTime);
        }
    }

    public void PlayerEntered()
    {
        if (state != States.Stun)
        {
            time = 4f;
            state = States.Chase;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hurtbox")
        {
            HP -= other.GetComponentInParent<HurtboxInfo>().damage;

            enemyVel = Vector3.Normalize(transform.position-other.GetComponentInParent<Transform>().position)* other.GetComponentInParent<HurtboxInfo>().knockback;
            enemyVel.y = other.GetComponentInParent<HurtboxInfo>().knockback*0.2f;
            C_C.Move(Vector3.up*0.1f);

            if (other.GetComponentInParent<HurtboxInfo>().canStun)
            {
                state = States.Stun;
                time = 3.5f;
            }
        }

        if (other.tag == "Wall")
        {
            if (state == States.Stun)
            {
                enemyVel.x = -enemyVel.x / 2;
                enemyVel.z = -enemyVel.z / 2;

                HP -= 8;
            }
        }
    }

    /*void OnCollisionEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (state == States.Stun) {
                enemyVel.x = -enemyVel.x / 2;
                enemyVel.z = -enemyVel.z / 2;

                HP -= 8;
            }
        }
    }*/
}