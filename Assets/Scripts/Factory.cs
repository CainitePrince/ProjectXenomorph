using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public float ProductionTime;
    public GameObject Item;
    public float TimeLeft;
    public Transform SpawnPoint;

    private float nextProductionTime;

    void Start ()
    {
        nextProductionTime = Time.time + ProductionTime;	
	}
	
	void Update ()
    {
        TimeLeft = nextProductionTime - Time.time;

	    if (Time.time >= nextProductionTime)
        {
            Instantiate(Item, SpawnPoint.position, SpawnPoint.rotation);

            nextProductionTime = Time.time + ProductionTime;
        }
	}
}
