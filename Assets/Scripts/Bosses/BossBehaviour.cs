using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;


public enum BossState
{
    IDLE,
    FOLLOW,
    ATTACK,
    DIE
};


public class BossBehaviour : MonoBehaviour
{
    public BossState bossState = BossState.IDLE;

    public bool playerPresent = false;
    private MusicManager audioManager;


    GameObject player;
    private Animator animator;
    private Animation animation;


    private float attackRange = 3.0f;
    private bool inAttackCD = false;

    float bulletSpeed = 10.0f;

    int hp = 10;

    [SerializeField]
    private float attackCD = 1.0f;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();       
        animator.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager=FindObjectOfType<MusicManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Continuously evaluate the player's distance and update bossState accordingly
        if (playerPresent)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= attackRange)
            {
                bossState = BossState.ATTACK;
            }
            //else
            //{
            //    bossState = BossState.FOLLOW; // Add FOLLOW logic here in the future
            //}
        }
        //else
        //{
        //    bossState = BossState.IDLE;
        //}

        // State behavior
        switch (bossState)
        {
            case BossState.ATTACK:
                Attack();
                break;
            case BossState.FOLLOW:

                break;
            case BossState.IDLE:

                break;
            case BossState.DIE:
                Die();
                break;
        }


        animator.enabled = playerPresent;
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
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;

            Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();

            Vector2 direction = (player.transform.position - transform.position).normalized;

            rb.velocity = direction * bulletSpeed;

            bullet.GetComponent<BaseProjectile>().GetPlayer(player.transform);
            bullet.GetComponent<BaseProjectile>().isEnemyBullet = true;
            StartCoroutine(CoolDown());

        }
    }


    public void Die()
    {
        RoomsManager.instance.StartCoroutine(RoomsManager.instance.RoomCoroutine());
        bossState = BossState.DIE;
        Destroy(gameObject);
        audioManager.changeToMain();
    }

    public void DamageBoss(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Die();
        }

    }
}
