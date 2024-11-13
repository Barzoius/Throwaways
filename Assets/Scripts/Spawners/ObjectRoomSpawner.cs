using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{

   [System.Serializable]
   public struct RandomSpawner
    {
        public string name;
        public SpawnerMetrics spawnMetrics;
    }

    public GridManager gridManager;

    public RandomSpawner[] spawnerData;


    private void Start()
    {
        //gridManager = GetComponent<GridManager>();
    }

    public void InitialiseObjectSpawners()
    {
        foreach (var spawner in spawnerData) 
        {
            SpawnObjects(spawner);
        }
    }

    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnMetrics.minSpawns,
                                           data.spawnMetrics.maxSpawns + 1);

        for(int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, gridManager.freeTiles.Count - 1);

            GameObject go = Instantiate(data.spawnMetrics.spawableObj,
                                        gridManager.freeTiles[randomPos],
                                        Quaternion.identity, transform) as GameObject;

            gridManager.freeTiles.RemoveAt(randomPos);
        }
    }
}
