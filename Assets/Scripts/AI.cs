using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator anim;
    private Transform player;
    private State currentState;
    private AudioSource audioS;
    [SerializeField] private ScriptableObject nPCStats;



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {
        NPCStats _npcStats = (NPCStats)nPCStats;
        currentState = new Idle(this.gameObject, anim, agent, player,audioS, _npcStats );
    }

    void Update()
    {
        currentState = currentState.Process();
    }
}
