using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stats {
	public float viewRange;
	public int maxHitpoints;
	public int hitpoints;
	public float influenceMultiplier;
}


[System.Serializable]
public struct UnitStats {
	public float moveSpeed;
}

[System.Serializable]
public struct WorkerStats {
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
public struct MilitaryStats {
	public float minAttackRange;
	public float maxAttackRange;
	public float attackStrength;
	public float attackRate;
}

[System.Serializable]
public struct SupportStats {
	public float abilityRate;
}

[System.Serializable]
public struct StubStats {
	public float spawnRate;
}