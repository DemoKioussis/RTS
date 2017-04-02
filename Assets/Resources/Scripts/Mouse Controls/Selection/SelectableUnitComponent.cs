﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(UnitController))]
public class SelectableUnitComponent : MonoBehaviour
{
    public Interactable interactable;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    public GameObject selectionCircle;
}