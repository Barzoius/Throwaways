using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;

    void Start()
    {
        StartCoroutine(DeleteDelay());
    }

    void Update()
    {
        
    }

    IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
