using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class Stats {
	public float viewRange;
    public float viewRangeMin;
    public float viewRangeMax;

    public int maxHitpoints;
	public float hitpoints;
	public float influenceMultiplier;
	public int paperCost;
	public int glueCost;
}
	
[System.Serializable]
public class UnitStats{
	public float moveSpeed;
	public float trainingTime;
	public int populationCost;
}

[System.Serializable]
public class WorkerStats {
	public float buildRate;
	public float buildStep;
	public float repairRate;
	public float repairStep;
	public float glueGatherRate;
	public float glueGatherStep;
	public float paperGatherRate;
	public float paperGatherStep;
}

[System.Serializable]
public class MilitaryStats {
	public float attackRange;
    public float AttackRangeMin;
    public float AttackRangeMax;

    public float attackStrength;
	public float attackRate;
}

[System.Serializable]
public class ResourceBuildingStats {
	public float gatheringTime;
	public int glueReturn;
	public int paperReturn;
}

[System.Serializable]
public class ResourceStats : Stats{
	public string type;
	public int quantity;
}

[System.Serializable]
public class MapStats : Stats{
	// map stats, if we ever do this
}

[System.Serializable]
public class SupportStats {
	public float abilityRate;
}

[System.Serializable]
public class StubStats {
	public float spawnRate;
}