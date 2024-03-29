using System;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int index = -1;
    GameObject[] checkPoints;
    float closestWaypoint = Mathf.Infinity;

    public Patrol(GameObject _npc, Animator _anim, NavMeshAgent _agent, Transform _player, AudioSource _audio, NPCStats _npcStats) 
    : base(_npc, _anim, _agent, _player, _audio, _npcStats)
    {
        name = STATE.PATROL;
        // index = 0;
    }

    public override void Enter()
    {
        base.Enter();
        index = -1;
        checkPoints = StatesGameEnvironment.Singleton.Checkpoints;
        anim.SetTrigger("isWalking");
        agent.isStopped = false;
        agent.speed = 2;
    }

    private void FindClosestWayPoint()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            Vector3 npcPosition = npc.transform.position;
            Vector3 waypointPosition = checkPoints[i].transform.position;
            float distanceToWaypoint = Vector3.Distance(npcPosition,waypointPosition);

            if(distanceToWaypoint < closestWaypoint)
            {
                closestWaypoint = distanceToWaypoint;
                index = i-1;

                if(index<0)
                {
                    index=0;
                }

                Debug.Log($"ClosestWaypoint: {checkPoints[index].name} has index {index}");
            }
        }
    }
    public override void Update()
    {
        if(index > checkPoints.Length -1 || index < 0)
        {
            index = 0;
        }
    
        if(agent.remainingDistance <1)
        {
            index ++;
            Debug.Log($"CurrentIndex: {index}");
        }

        agent.SetDestination(checkPoints[index].transform.position);

        if(CanSeePlayer())
        {
            nextState = new Pursue(npc,anim, agent, player, audioSource, npcStats);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.ResetTrigger("isWalking");
    }

}
