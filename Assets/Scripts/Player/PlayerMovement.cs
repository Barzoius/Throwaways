using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float bulletSpeed;


    [SerializeField]
    private float fireDelay;

    Rigidbody2D rigidbody;

    public GameObject bulletPrefab;

    public Text hitpoints;

    private float lastFire;


    public Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        animator.SetFloat("FrontMovement", Mathf.Abs(vertical * speed));

        if (Mathf.Abs(vertical * speed) > 0)
        {
            if (vertical < 0)
            {
                animator.SetInteger("Direction", 1); // down

            }
            else if (vertical > 0)
            {
                animator.SetInteger("Direction", 2); // up
            }
        }
        
        animator.SetFloat("SideMovement", Mathf.Abs(horizontal * speed));

        if (Mathf.Abs(horizontal * speed) > 0)
        {
            if (horizontal < 0)
            {
                animator.SetInteger("Direction", 3); // left

            }
            else if (horizontal > 0)
            {
                animator.SetInteger("Direction", 4); // right
            }
        }

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }


        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0.0f);


    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0 );
    }
}
