using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> Obstacles {get => obstacles;}
    private	GameObject[] goalLocations;


    public static GameEnvironment Singleton 
    {
        get{
            if(instance == null)
            {
                instance = new GameEnvironment();
                instance.goalLocations = GameObject.FindGameObjectsWithTag("goal");
            }
            return instance;
        }
    }

    public GameObject GetRandomGoal()
    {
        int index = Random.Range(0, goalLocations.Length);
        GameObject goal = goalLocations[index];
        return goal;
    }

    public void AddObstacle(GameObject go)
    {
        obstacles.Add(go);
    }

    public void RemoveObstacle(GameObject go)
    {
        int index = obstacles.IndexOf(go);
        obstacles.RemoveAt(index);
        GameObject.Destroy(go);
    }
}
