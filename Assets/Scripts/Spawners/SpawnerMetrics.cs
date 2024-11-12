using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerMetrics : ScriptableObject
{

    public GameObject spawableObj;

    public int minSpawns;
    public int maxSpawns;
}
