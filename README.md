| <img src="ResourcesMD/GAME_ICON.png" width="120px"/> | **THROWAWAYS** |
|------------------------------------------------------|--------------------------|

-------------------------------
## Game Presentation

Throwaways is a 2D roguelike, twin-stick shooter with a top-down view 
where players explore
 procedurally generated dungeons filled with a mix 
 of strange monsters and bosses.
 Throwaways emphasizes randomized loot drops and 
 power-ups that dramatically change gameplay with each run, creating a unique 
 experience every time.
 The game combines fast-paced combat with strategic decision-making, 
 as players choose which items to keep or toss, affecting both combat 
 and character stats.

-------------------------------
## Inputs

### MOVE
- **W** - UP
- **S** - DOWN
- **A** - LEFT
- **D** - RIGHT

### SHOOT
- **↑** - UP
- **↓** - DOWN
- **←** - LEFT
- **→** - RIGHT

-------------------------------

## Build:
UNITY EDITOR VERSION: 2022.3.40f1<br />

The game demo can be played in browser here: https://barzoius.itch.io/throwaways<br />
You will need this password: throwaways


## Technical Contents

1. [Player Movement](#plyer-shooting)
2. [Shooting](#shooting)
3. [Map Generation](#map-generation)

-------------------------------

# Player Movement

We first create our input vector:

 ```cs
    float speed = GameManager.currentMS;

    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    Vector2 inputVector 
        = new Vector2(horizontal, vertical).normalized; 
 ```
We normalize the input to ensure consistent speed.

Next, we account for diagonal movement to correctly adjust the speed.
 
```cs
    bool isDiagonal = horizontal != 0 && vertical != 0;

    float adjustedSpeed 
        = isDiagonal ? speed / Mathf.Sqrt(2) : speed;
```

Now, we calculate the desired speed values and interpolate between the current speed and the target values for animation purposes. This allows for a smooth transition from the idle state to a movement state when the speed is not zero.
```cs

    float targetFrontMovement 
        = Mathf.Abs(inputVector.y * adjustedSpeed);
    float targetSideMovement 
        = Mathf.Abs(inputVector.x * adjustedSpeed);

    animator.SetFloat("FrontMovement", 
        Mathf.Lerp(animator.GetFloat("FrontMovement"), 
                   targetFrontMovement, 
                   Time.deltaTime * 10f));
   
    animator.SetFloat("SideMovement", 
        Mathf.Lerp(animator.GetFloat("SideMovement"), 
                   targetSideMovement,
                   Time.deltaTime * 10f));
```
And for animation purposes again we set the movement direction:

```cs
        if (inputVector != Vector2.zero)
        {
            if (Mathf.Abs(inputVector.y) >= Mathf.Abs(inputVector.x))  // Vertical
            {
                animator.SetInteger("Direction", 
                                    inputVector.y < 0 ? 1 : 2); // Down or Up
            }
            else // Horizontal
            {
                animator.SetInteger("Direction", 
                                    inputVector.x < 0 ? 3 : 4); // Left or Right
            }
        }
```
It is important to note that the transitions between directional movement animations are done without transition time or animation blending.

Returning to the actual rigidbody velocity, we interpolate between the current velocity vector and the target velocity vector. This interpolation ensures a smooth transition between stationary and non-stationary states, preventing the character from stopping abruptly.
```cs
    float smoothingFactor = 5f;

    rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, 
                                    new Vector3(inputVector.x * adjustedSpeed, 
                                                inputVector.y * adjustedSpeed, 0.0f),
                                                Time.deltaTime * smoothingFactor);
```

The result: 

<img src="ResourcesMD/DanMovement.gif" alt="Framework Diagram" style="width:100%;">

# Shooting


In order to optimize the shooting action an 
object pooling pattern was used through a class Pool that
has two main methods:

The `Start()` method:

```cs
    void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);

            Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            obj.SetActive(false);


            poolList.Add(obj);

        }
    }
```
Which fills the pool with projectile objects.

And the `GetPooled()` method:

```cs
    public GameObject GetPooled()
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeInHierarchy) // not active
            {
                return poolList[i];
            }
        }

        return null;
    }
```
Which searches the pool for inactive objects. 
In the case that it doesn't finds one it returns null. 
We could have chose to create a new object and add it to the pool
but because of the choice in pool size, bullet fire rate, 
bullet speed and bullet delay the pool doesn't have time to activat all obejcts.

The Shooting action itself:

```cs

    void Shoot(float x, float y)
    {
        Vector2 shootDirection = new Vector2(x, y).normalized;

        // Offset the bullet starting position in the direction of shooting
        Vector3 bulletStartPosition = transform.position + 
                new Vector3(shootDirection.x, shootDirection.y, 0) * 0.5f;

        GameObject bullet = Pool.instance.GetPooled();

        if (bullet != null)
        {
            bullet.transform.position = bulletStartPosition;
            bullet.transform.rotation = Quaternion.identity;

            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * bulletSpeed;

            int direction = (x > 0) ? 4 : (x < 0) ? 3 : (y > 0) ? 2 : 1;
            animator.SetInteger("ShootDirection", direction);

            StartCoroutine(ResetShootDirection());
        }

    }
```
The result: 

<img src="ResourcesMD/shoot.gif" alt="Framework Diagram" style="width:100%;">



# Map Generation


## Credits

### Authors

Ghinea Theodor Traian - UI
<br />
Moisel Rares-Ioan - Gamplay & Art
<br />
Bretfelean Rares - Audio

### Assets

Chests - https://cmski.itch.io/tabletop-chests
<br />
Doors - https://steamcommunity.com/id/snoochSDK/
<br />
Speed/Health items -
<br />
Bat enemy(modifed) -

-The rest of the assets were done by us-


