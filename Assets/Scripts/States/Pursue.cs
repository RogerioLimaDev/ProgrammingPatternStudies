using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    public Pursue(GameObject _npc, Animator _anim, NavMeshAgent _agent, Transform _player, AudioSource _audio, NPCStats _npcStats) 
    : base(_npc, _anim, _agent, _player, _audio, _npcStats)
    {
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        agent.isStopped = false;
        agent.speed = npcStats.agentRunSpeed;
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.transform.position);
        agent.stoppingDistance = 1.0f;


        if(agent.hasPath)
        {
            if(CanAttack())
            {
                nextState = new Attack(npc, anim, agent, player, audioSource, npcStats);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Idle(npc, anim, agent, player, audioSource, npcStats);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
