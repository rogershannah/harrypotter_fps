using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        agent = GetComponent<NavMeshAgent>();

        Animator anim = GetComponent<Animator>();
        anim.SetInteger("animState", 1);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
