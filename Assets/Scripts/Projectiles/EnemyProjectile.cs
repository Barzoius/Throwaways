using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float lifetime;

    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        curPos=transform.position;
        transform.position=Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
        if(curPos==lastPos){
            Destroy(gameObject);
        }
        lastPos=curPos;
    }

    public void GetPlayer(Transform player){
        
        playerPos= player.position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("Hit Player!");

            GameManager.DamagePlayer(5);
            Destroy(gameObject);
        }
    }
}
