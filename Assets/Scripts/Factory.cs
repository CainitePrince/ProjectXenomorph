using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public float ProductionTime;
    public GameObject Item;

    private float nextProductionTime;

    void Start ()
    {
        nextProductionTime = Time.time + ProductionTime;	
	}
	
	void Update ()
    {
	    if (Time.time >= nextProductionTime)
        {
            Instantiate(Item);

            nextProductionTime = Time.time + ProductionTime;
        }
	}
}
