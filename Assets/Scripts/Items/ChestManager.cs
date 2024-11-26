using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{

    [System.Serializable]
    public struct Spawnable
    {
        public GameObject go;

        public float weight;
    }

    Rigidbody2D rigidbody;
    Animator animator;



    public List<Spawnable> items = new List<Spawnable>();

    float totalWeight;

    private bool isOpened = false;

    private void Awake()
    {
        totalWeight = 0f;
        foreach (var item in items)
        {
            totalWeight += item.weight;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the chest
        if (collision.CompareTag("Player") && !isOpened)
        {
            animator.Play("OpenChest");
            isOpened = true; // Ensure the chest only opens once
            SpawnItem();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void SpawnItem()
    {

        // play animation

        float pick = Random.value * totalWeight;

        int chosenIndex = 0;
        float cumulativeWeight = items[chosenIndex].weight;

        while (pick > cumulativeWeight && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;
        }

        Vector3 spawnPosition = transform.position + new Vector3(1.2f, 1.2f);

        GameObject iGo = Instantiate(items[chosenIndex].go,
                                      spawnPosition,
                                      Quaternion.identity) as GameObject;

        
       // Destroy(gameObject);
    }
}



