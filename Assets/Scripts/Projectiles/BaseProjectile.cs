using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;

    public Animator animator;

    private bool isDestroying = false;

    public bool isEnemyBullet = false;

    private Vector2 lastPos;
    private Vector2 currentPos;
    private Vector2 playerPos;

    void Start()
    {
        if(gameObject.activeSelf)
            StartCoroutine(DeleteDelay());

        //if (!isEnemyBullet)
        //{

        //}

        
    }

    void Update()
    {
        if(isEnemyBullet)
        {
            currentPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f* Time.deltaTime);

            if(currentPos == lastPos)
            {
                Destroy(gameObject);
                //gameObject.SetActive(false);
            }

            lastPos = currentPos;

        }

        if (gameObject.activeSelf)
            StartCoroutine(DeleteDelay());
    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }


    IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(lifeTime);

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy" && !isEnemyBullet)
        {
            Debug.Log("Hit an enemy!");

            EnemyBehaviour enemyBehaviour = collider.gameObject.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.Die();
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Enemy does not have an EnemyBehaviour script attached.");
            }
        }

        if(collider.tag == "Boss" && !isEnemyBullet)
        {
            BossBehaviour BB = collider.gameObject.GetComponent<BossBehaviour>();

            if(BB != null)
            {
                BB.DamageBoss(2);
                gameObject.SetActive(false);
            }
        }

        if (collider.tag == "Wall")
        {
      
           gameObject.SetActive(false);

        }

        if (collider.tag == "MidWallCollider")
        {

            gameObject.SetActive(false);

        }

        if (collider.tag == "Player" && isEnemyBullet)
        {
            GameManager.DamagePlayer(2);
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}
