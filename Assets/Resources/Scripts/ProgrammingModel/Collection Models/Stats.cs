using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class Stats {
	public float viewRange;
	public int maxHitpoints;
	public int hitpoints;
	public float influenceMultiplier;
}


[System.Serializable]
public class UnitStats {
	public float moveSpeed;
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
	public float minAttackRange;
	public float maxAttackRange;
	public float attackStrength;
	public float attackRate;
}

[System.Serializable]
public class SupportStats {
	public float abilityRate;
}

[System.Serializable]
public class StubStats {
	public float spawnRate;
}