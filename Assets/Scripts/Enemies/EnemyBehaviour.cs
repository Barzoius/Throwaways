using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    WANDER,
    FOLLOW,
    RETREAT,
    DIE
};

public class EnemyBehaviour : MonoBehaviour
{

    GameObject player;

    public EnemyState enemyState = EnemyState.WANDER;

    [SerializeField]
    private float aggroRange;

    [SerializeField]
    private float speed;


    private bool chooseDir = false;
    private bool isDead = false;

    private Vector3 randomDir;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.WANDER:
                Wander();
                break;
            case EnemyState.FOLLOW:
                Follow();
                break;
            case EnemyState.RETREAT:
                Retreat();
                break;
            case EnemyState.DIE:
                Die();
                break;

        }

        if (inRange(aggroRange) && enemyState != EnemyState.DIE)
        {
            enemyState = EnemyState.FOLLOW;
        }
        else if (!inRange(aggroRange) && enemyState != EnemyState.DIE)
        {
            enemyState = EnemyState.WANDER;
        }
    }

    private bool inRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, 
                                             nextRotation,
                                             Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Wander()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

        if(inRange(aggroRange))
        {
            enemyState = EnemyState.FOLLOW;
        }
    }


    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                  player.transform.position,
                                                  speed * Time.deltaTime);
    }


    void Retreat()
    {

    }


    public void Die()
    {
        enemyState = EnemyState.DIE;
        Destroy(gameObject);
    }
}
