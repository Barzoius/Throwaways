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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            Debug.Log("Hit an enemy!");

            EnemyBehaviour enemyBehaviour = collider.gameObject.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.Die();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Enemy does not have an EnemyBehaviour script attached.");
            }
        }
        if(collider.tag == "EnemyShooter")
        {
            Debug.Log("Hit an enemy!");

            EnemyShooterBehaviour enemyBehaviour = collider.gameObject.GetComponent<EnemyShooterBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.Die();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Enemy does not have an EnemyBehaviour script attached.");
            }
        }
    }
}
