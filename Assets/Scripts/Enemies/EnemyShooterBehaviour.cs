using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyShState
{
    WANDER,
    FOLLOW,
    ATTACK,
    RETREAT,
    DIE
};

public class EnemyShooterBehaviour : MonoBehaviour
{
    GameObject player;

    public EnemyShState enemyshState = EnemyShState.WANDER;

    [SerializeField]
    private float aggroRange;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private float attackCD;

    private bool inAttackCD = false;

    [SerializeField]
    private float speed;


    private bool chooseDir = false;
    private bool isDead = false;

    private Vector3 randomDir;
    public GameObject bulletPrefab;
    public float bulletSpeed;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        switch (enemyshState)
        {
            case EnemyShState.WANDER:
                Wander();
                break;
            case EnemyShState.FOLLOW:
                Follow();
                break;
            case EnemyShState.ATTACK:
                Attack();
                break;
            case EnemyShState.RETREAT:
                Retreat();
                break;
            case EnemyShState.DIE:
                Die();
                break;

        }

        if (inRange(aggroRange) && enemyshState != EnemyShState.DIE)
        {
            enemyshState = EnemyShState.FOLLOW;
        }
        else if (!inRange(aggroRange) && enemyshState != EnemyShState.DIE)
        {
            enemyshState = EnemyShState.WANDER;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            enemyshState = EnemyShState.ATTACK;
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
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

        if (inRange(aggroRange))
        {
            enemyshState = EnemyShState.FOLLOW;
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
        if(!inAttackCD){
            GameObject bullet=Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<EnemyProjectile>().GetPlayer(player.transform);
            bullet.AddComponent<Rigidbody2D>().gravityScale=0;
            
            StartCoroutine(CoolDown());
        }

    }

    void Retreat()
    {

    }


    public void Die()
    {
        enemyshState = EnemyShState.DIE;
        Destroy(gameObject);
    }
}
