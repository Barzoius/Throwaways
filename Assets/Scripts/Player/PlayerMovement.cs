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

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }

        float smoothingFactor = 5f;

        /// smooth update of velocity for smooth stoping
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, 
                                          new Vector3(inputVector.x * adjustedSpeed, 
                                                      inputVector.y * adjustedSpeed, 0.0f),
                                          Time.deltaTime * smoothingFactor);

        /// the varaint without smooth stopping
        // rigidbody.velocity = new Vector3(inputVector.x * adjustedSpeed, inputVector.y * adjustedSpeed, 0.0f);



        //animator.SetFloat("FrontMovement", Mathf.Abs(vertical * adjustedSpeed));

        //if (Mathf.Abs(vertical * adjustedSpeed) > 0)
        //{
        //    if (vertical < 0)
        //    {
        //        animator.SetInteger("Direction", 1); // down

        //    }
        //    else if (vertical > 0)
        //    {
        //        animator.SetInteger("Direction", 2); // up
        //    }
        //}

        //animator.SetFloat("SideMovement", Mathf.Abs(horizontal * adjustedSpeed));

        //if (Mathf.Abs(horizontal * adjustedSpeed) > 0)
        //{
        //    if (horizontal < 0)
        //    {
        //        animator.SetInteger("Direction", 3); // left

        //    }
        //    else if (horizontal > 0)
        //    {
        //        animator.SetInteger("Direction", 4); // right
        //    }
        //}

        //float shootHorizontal = Input.GetAxis("ShootHorizontal");
        //float shootVertical = Input.GetAxis("ShootVertical");

        //if((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
        //{
        //    Shoot(shootHorizontal, shootVertical);
        //    lastFire = Time.time;
        //}


        //rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0.0f);


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
