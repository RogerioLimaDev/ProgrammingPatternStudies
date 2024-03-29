using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.Rendering;

public sealed class StatesGameEnvironment 
{
    private static StatesGameEnvironment instance;
    private GameObject[] checkPoints ;
    public GameObject[] Checkpoints { get => checkPoints;}
    public static StatesGameEnvironment Singleton
    {
        get
        {
            if(instance == null)
            {
                instance =  new StatesGameEnvironment();
                instance.checkPoints = GameObject.FindGameObjectsWithTag("Checkpoint");
                instance.checkPoints = instance.checkPoints.OrderBy(waypoint => waypoint.name).ToArray();
            }
            return instance;
        }

    }
}
  
  
