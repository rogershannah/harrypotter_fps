using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle, Patrol, Chase, Attack, Dead
    }

    public FSMStates currentState;
    public float enemySpeed = 3.5f;
    public float chaseSpeed = 5;
    public float chaseDistance = 10;
    public float attackDistance = 3;
    public GameObject player;
    public GameObject[] spellProjectiles;
    public GameObject wandTip;
    public float shootRate = 2;
    public GameObject deadVFX;
    public Transform enemyEyes;
    public float fieldOfView = 45f;


    GameObject[] wanderPoints;
    Animator anim;
    Vector3 nextDestination;
    int curDestIndex = 0;
    float distanceToPlayer;
    float elapsedTime;

    EnemyHealth enemyHealth;
    int health;
    Transform deadTransform;
    NavMeshAgent agent;

    bool isDead;
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        health = enemyHealth.currentHealth;

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    void Initialize()
    {

        agent = GetComponent<NavMeshAgent>();
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        wandTip = GameObject.FindGameObjectWithTag("WandTip");

        currentState = FSMStates.Patrol;
        FindNextPoint();

        enemyHealth = GetComponent<EnemyHealth>();
        health = enemyHealth.currentHealth;
        isDead = false;
    }
    void UpdatePatrolState()
    {
        print("Patrolling");

        anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;
        agent.speed = enemySpeed;

        if(Vector3.Distance(transform.position, nextDestination) < 2)
        {
            FindNextPoint();
        }
        else if(distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
        agent.SetDestination(nextDestination);
    }

    void UpdateChaseState()
    {
        print("Chasing!");

        anim.SetInteger("animState", 2);

        nextDestination = player.transform.position;

        agent.stoppingDistance = attackDistance;
        agent.speed = chaseSpeed;

        if (distanceToPlayer <= attackDistance && IsPlayerInClearFOV())
        {
            currentState = FSMStates.Attack;
        } else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        print("Attack!");

        anim.SetInteger("animState", 3);

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);

        //enemy shoot projectile at player
        EnemySpellCast();
    }

    void UpdateDeadState()
    {
        isDead = true;
        anim.SetInteger("animState", 4);
        deadTransform = gameObject.transform;

        Destroy(gameObject, 3);
    }

    private void OnDestroy()
    {
        Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

    void FindNextPoint()
    {
        Debug.Log(curDestIndex);
        curDestIndex = (curDestIndex + 1) % wanderPoints.Length;
        nextDestination = wanderPoints[curDestIndex].transform.position;

        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemySpellCast()
    {
        if (!isDead)
        {
            if (elapsedTime >= shootRate)
            {
                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("SpellCasting", animDuration);
                elapsedTime = 0.0f;
            }

        }
    }

    void SpellCasting()
    {
        int randomIndex = Random.Range(0, spellProjectiles.Length);
        GameObject spellProjectile = spellProjectiles[randomIndex];
              

        Instantiate(spellProjectile, wandTip.transform.position, wandTip.transform.rotation);
    }

    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        //chase
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        //fov rays
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if(Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if(Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance)){
                if (hit.collider.CompareTag("Player"))
                {
                    print("Player in sight!");
                    return true;
                } else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
