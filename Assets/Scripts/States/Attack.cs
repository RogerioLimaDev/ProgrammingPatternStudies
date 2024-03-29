using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float timeToShoot;
    public Attack(GameObject _npc, Animator _anim, NavMeshAgent _agent, Transform _player, AudioSource _audio, NPCStats _npcStats) 
    : base(_npc, _anim, _agent, _player, _audio, _npcStats)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        base.Enter();
        agent.isStopped = true;
        timeToShoot = ResetTimeToShoot();
    }

    private float ResetTimeToShoot() 
    {
        timeToShoot = (npcStats.shootingInterval)*1000 + Time.timeSinceLevelLoad;
        return timeToShoot;
    }

    public override void Update()
    {
        Vector3 playerDirection = npc.transform.position - player.transform.position;
        playerDirection.y = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(-playerDirection), Time.deltaTime*(npcStats.agentRotationSpeed));
        anim.ResetTrigger("isShooting");
        anim.ResetTrigger("isIdle");
        timeToShoot = timeToShoot - Time.timeSinceLevelLoad;

        if(timeToShoot <= 0 ) 
        {   
            anim.SetTrigger("isShooting");
            audioSource.PlayOneShot(audioSource.clip);
            timeToShoot = ResetTimeToShoot();
        }

        if(!CanAttack())
        {
            nextState = new Idle( npc, anim, agent, player, audioSource, npcStats);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        audioSource.Stop();
        anim.ResetTrigger("isShooting");
        base.Exit();
    }
}
   
   

