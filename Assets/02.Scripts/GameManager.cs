using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Transform[] points;
    public List<Transform> points = new List<Transform>();
    public GameObject monster;
    public float createTime = 3.0f;
    private bool isGameOver;

    public bool IsGameOver
    {
        get{return isGameOver;}
        set{
            isGameOver=value;
            if(isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        //points = spawnPointGroup?.GetComponentsInChildren<Transform>();
        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);
        foreach(Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
        InvokeRepeating("CreateMonster",2.0f,createTime);
       
    }
    
    void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);
        Instantiate(monster,points[idx].position,points[idx].rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
