using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool instance;

    private List<GameObject> poolList = new List<GameObject>();

    private int poolSize = 20;

    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        if (instance == null)
            instance = this; 
    }

    void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);

            Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            obj.SetActive(false);


            poolList.Add(obj);

        }
    }

    public GameObject GetPooled()
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeInHierarchy) // not active
            {
                return poolList[i];
            }
        }

        return null;
    }

}
