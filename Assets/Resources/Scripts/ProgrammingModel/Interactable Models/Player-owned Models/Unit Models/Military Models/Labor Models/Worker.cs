﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Worker : Unit {

	
	public WorkerStats workerStats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void InteractWith(Interactable target)
	{
	}

	protected override float Influence(Vector3 samplePosition)
	{
		return 0;
	}

}
