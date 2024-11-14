using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    private string currentState;


    private bool isShooting = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }


    void Update()
    {
        speed = GameManager.currentMS;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical).normalized; // for consistent speed

        bool isDiagonal = horizontal != 0 && vertical != 0;

        float adjustedSpeed = isDiagonal ? speed / Mathf.Sqrt(2) : speed;


        float targetFrontMovement = Mathf.Abs(inputVector.y * adjustedSpeed);
        float targetSideMovement = Mathf.Abs(inputVector.x * adjustedSpeed);

        animator.SetFloat("FrontMovement", Mathf.Lerp(animator.GetFloat("FrontMovement"), targetFrontMovement, Time.deltaTime * 10f));
        animator.SetFloat("SideMovement", Mathf.Lerp(animator.GetFloat("SideMovement"), targetSideMovement, Time.deltaTime * 10f));

        if (targetFrontMovement > 0)
        {
            animator.SetInteger("Direction", inputVector.y < 0 ? 1 : 2); // down  up
        }
        else if (targetSideMovement > 0)
        {
            animator.SetInteger("Direction", inputVector.x < 0 ? 3 : 4); // left  right
        }


        float smoothingFactor = 5f;

        /// smooth update of velocity for smooth stoping
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, 
                                          new Vector3(inputVector.x * adjustedSpeed, 
                                                      inputVector.y * adjustedSpeed, 0.0f),
                                          Time.deltaTime * smoothingFactor);

        /// the varaint without smooth stopping
        // rigidbody.velocity = new Vector3(inputVector.x * adjustedSpeed, inputVector.y * adjustedSpeed, 0.0f);


        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 ^ shootVertical != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }

    }

    void Shoot(float x, float y)
    {
        Vector2 shootDirection = new Vector2(x, y).normalized;

        // offset the bullet starting position in the direction of shooting
        Vector3 bulletStartPosition = transform.position + new Vector3(shootDirection.x, shootDirection.y, 0) * 0.5f;

        GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, transform.rotation);

        Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        rb.velocity = shootDirection * bulletSpeed;

        int direction = (x > 0) ? 4 : (x < 0) ? 3 : (y > 0) ? 2 : 1;
        animator.SetInteger("ShootDirection", direction);


        StartCoroutine(ResetShootDirection());
    }


    // not finished better shooting offseting 

    //void Shoot(float x, float y)
    //{
    //    Vector2 shootDirection = new Vector2(x, y).normalized;

    //    // offset the bullet starting position in the direction of shooting
    //    Vector3 bulletStartPosition = transform.position + new Vector3(shootDirection.x, shootDirection.y, 0) * 0.5f;

    //    GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, transform.rotation);

    //    Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
    //    rb.gravityScale = 0;

    //    // get the player velocity and add it to the bullet velocity
    //    Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
    //    Vector2 playerVelocity = playerRb != null ? playerRb.velocity : Vector2.zero;

    //    rb.velocity = (shootDirection * bulletSpeed) + playerVelocity;

    //    int direction = (x > 0) ? 4 : (x < 0) ? 3 : (y > 0) ? 2 : 1;
    //    animator.SetInteger("ShootDirection", direction);

    //    StartCoroutine(ResetShootDirection());
    //}

    IEnumerator ResetShootDirection()
    {
        yield return new WaitForSeconds(0.2f); // delay 
        animator.SetInteger("ShootDirection", 0); // reset to idle
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
