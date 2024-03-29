using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject _npc, Animator _anim, NavMeshAgent _agent, Transform _player, AudioSource _audio, NPCStats _npcStats) 
    : base(_npc, _anim, _agent, _player, _audio, _npcStats)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("isIdle");
    }

    public override void Update()
    {
        if(CanSeePlayer()) 
        {
            nextState = new Pursue(npc, anim, agent, player, audioSource, npcStats);
            stage = EVENT.EXIT;
        }
        
        if(Random.Range(0,100) < 10)
        {
            nextState = new Patrol(npc,anim,agent, player, audioSource, npcStats);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.ResetTrigger("isIdle");
    }

}
