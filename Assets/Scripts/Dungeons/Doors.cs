using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public enum DoorPlacemant
    { 
        TOP, BOTTOM, LEFT, RIGHT
    }

    public enum DoorType
    {
        NORMAL_OPENED, NORMAL_CLOSED, CHEST, BOOS 
    }

    public DoorPlacemant doorPlacemant;

    private GameObject player;

    public GameObject doorCollider;

    private float widthOffset = 4.2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            switch(doorPlacemant)
            {
                case DoorPlacemant.TOP:
                    player.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y + widthOffset);
                    break;
                case DoorPlacemant.BOTTOM:
                    player.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y - widthOffset);
                    break;
                case DoorPlacemant.LEFT:
                    player.transform.position = new Vector3(transform.position.x - widthOffset,
                                                            transform.position.y);
                    break;
                case DoorPlacemant.RIGHT:
                    player.transform.position = new Vector3(transform.position.x + widthOffset,
                                                            transform.position.y);
                    break;
            }
        }
    }
}
