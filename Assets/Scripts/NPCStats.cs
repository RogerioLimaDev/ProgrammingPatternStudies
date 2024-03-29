using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCStats", menuName = "NPCStats", order = 0)]
public class NPCStats : ScriptableObject 
{
    public float agentSpeed;
    public float agentRotationSpeed;
    public float agentRunSpeed;
    public float visionDistance;
    public float visionAngle;
    public float shootingDistance;
    public float shootingInterval;
}
