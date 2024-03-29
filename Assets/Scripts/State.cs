using UnityEngine;
using UnityEngine.AI;

public class State
{
    public STATE name;
    protected EVENT stage;
    protected Animator anim;
    protected GameObject npc;
    protected NavMeshAgent agent;
    protected Transform player;
    protected AudioSource audioSource;
    protected NPCStats npcStats;
    protected State nextState;
    Vector3 playerDistance;

   public enum STATE
   {
        IDLE,
        PATROL,
        PURSUE,
        ATTACK,
        SLEEP
   }

   public enum EVENT 
   {
        ENTER,
        UPDATE,
        EXIT
   }

   public State(GameObject _npc, Animator _anim, NavMeshAgent _agent, Transform _player, AudioSource _audio, NPCStats _npcStats)
   {
        player = _player;
        npc = _npc;
        anim = _anim;
        agent = _agent;
        audioSource = _audio;
        npcStats = _npcStats;
        stage = EVENT.ENTER;
   }

   public virtual void Enter() { stage = EVENT.UPDATE; Debug.Log(name);}
   public virtual void Update() { stage = EVENT.UPDATE;}
   public virtual void Exit() { stage = EVENT.EXIT;}

   public State Process() 
   {
        switch (stage)
        {
            case EVENT.ENTER:
                Enter();
                break;
            case EVENT.UPDATE:
                Update();
                break;
            case EVENT.EXIT:
                Exit();
                return nextState;
            default:
                break;
        }
        return this;
   }

   public bool CanSeePlayer() 
   {
        playerDistance = player.transform.position -  npc.transform.position;
        playerDistance.y = player.transform.position.y;
        float angleToPlayer = Vector3.Angle(playerDistance,npc.transform.forward);


        Debug.DrawLine(npc.transform.position,player.transform.position, Color.red);
        Debug.DrawLine(playerDistance, npc.transform.position, Color.green);

        if(playerDistance.magnitude < npcStats.visionDistance && angleToPlayer < npcStats.visionAngle)
        {
            // Debug.Log("NPC can see the player");
            return true;
        }
        return false;
   }

   public bool CanAttack() 
   {
        Vector3 playerDirection = npc.transform.position - player.transform.position;

        if(playerDirection.magnitude < npcStats.shootingDistance)
        {
            return true;
        }
        return false;
   }
}
