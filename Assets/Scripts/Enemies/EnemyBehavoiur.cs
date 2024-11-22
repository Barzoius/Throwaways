using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    IDLE,
    WANDER,
    FOLLOW,
    ATTACK,
    RETREAT,
    DIE
};

public enum EnemyType
{
    MELEE,
    RANGED
};

public class EnemyBehaviour : MonoBehaviour
{

    GameObject player;

    public EnemyState enemyState = EnemyState.WANDER;

    public EnemyType enemyType;

    [SerializeField]
    private float aggroRange;

    [SerializeField]
    private float attackRange = 1.0f;

    [SerializeField]
    private float attackCD = 2.0f;

    private bool inAttackCD = false;

    [SerializeField]
    private float speed;

    public bool playerPresent = false;

    private bool chooseDir = false;
    private bool isDead = false;

    private Vector3 randomDir;

    public GameObject bulletPrefab;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.IDLE:
                Idle();
                break;
            case EnemyState.WANDER:
                Wander();
                break;
            case EnemyState.FOLLOW:
                Follow();
                break;
            case EnemyState.ATTACK:
                Attack();
                break;
            case EnemyState.RETREAT:
                Retreat();
                break;
            case EnemyState.DIE:
                Die();
                break;

        }

        if (playerPresent == true)
        {
            if (inRange(aggroRange) && enemyState != EnemyState.DIE)
            {
                enemyState = EnemyState.FOLLOW;
            }
            else if (!inRange(aggroRange) && enemyState != EnemyState.DIE)
            {
                enemyState = EnemyState.WANDER;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                enemyState = EnemyState.ATTACK;
            }
        }
        else
        {
            enemyState = EnemyState.IDLE;
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
        //Quaternion nextRotation = Quaternion.Euler(randomDir);
        //transform.rotation = Quaternion.Lerp(transform.rotation,
        //                                     nextRotation,
        //                                     Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Idle()
    {

    }

    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

        if (inRange(aggroRange))
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


    private IEnumerator CoolDown()
    {
        inAttackCD = true;
        yield return new WaitForSeconds(attackCD);
        inAttackCD = false;
    }

    void Attack()
    {

        if (!inAttackCD)
        {

            switch (enemyType)
            {
                case (EnemyType.MELEE):
                    GameManager.DamagePlayer(5);
                    StartCoroutine(CoolDown());
                    break;

                case (EnemyType.RANGED):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BaseProjectile>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BaseProjectile>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;

            }

        }
    }

    void Retreat()
    {

    }


    public void Die()
    {
        RoomsManager.instance.StartCoroutine(RoomsManager.instance.RoomCoroutine());
        enemyState = EnemyState.DIE;
        Destroy(gameObject);
    }
}