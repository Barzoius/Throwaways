using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [System.Serializable]
    public struct Spawnable
    {
        public GameObject go;

        public float weight;
    }

    public List<Spawnable> items = new List<Spawnable>();

    float totalWeight;


    private void Awake()
    {
        totalWeight = 0f;
        foreach (var item in items) 
        {
            totalWeight += item.weight;
        }
    }

    void Start()
    {
        float pick = Random.value * totalWeight;

        int chosenIndex = 0;

        float cumulativeWeight = items[chosenIndex].weight;

        while(pick > cumulativeWeight && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;
        }

        GameObject iGo = Instantiate(items[chosenIndex].go,
                                   transform.position,
                                   Quaternion.identity) as GameObject;
    }


    void Update()
    {
        
    }
}
