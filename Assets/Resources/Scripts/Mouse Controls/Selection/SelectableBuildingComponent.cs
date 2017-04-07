using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Building))]
public class SelectableBuildingComponent : MonoBehaviour
{
	public Interactable interactable;

	void Awake()
	{
		interactable = GetComponent<Interactable>();
	}

	public GameObject selectionCircle;
}