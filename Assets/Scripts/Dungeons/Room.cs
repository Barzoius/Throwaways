using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Door;
using static Room;

public class Room : MonoBehaviour
{
    [Serializable]
    public struct RoomMetrics
    {
        public float width;
        public float height;
        public int x;
        public int y;
    }

    public Door topDoor;
    public Door bottomDoor;
    public Door leftDoor;
    public Door rightDoor;

    public List<Door> doorsList = new List<Door>();

    public RoomMetrics roomMetrics = new RoomMetrics();

    public Room(int x, int y)
    {
        this.roomMetrics.x = x;
        this.roomMetrics.y = y;
    }

    private bool updatedDoors = false;

    void Start()
    {
        if(RoomsManager.instance == null)
        {
            return;
        }

        Door[] doorsArray = GetComponentsInChildren<Door>();

        foreach(Door door in doorsArray) 
        {
            doorsList.Add(door);

            switch(door.doorPlacemant) 
            {
                case Door.DoorPlacemant.TOP:
                    topDoor = door;
                    break;
                case Door.DoorPlacemant.BOTTOM:
                    bottomDoor = door;
                    break;
                case Door.DoorPlacemant.LEFT:
                    leftDoor = door;
                    break;
                case Door.DoorPlacemant.RIGHT:
                    rightDoor = door;
                    break;
                        

            }
        }

        RoomsManager.instance.RegisterRoom(this);
    }

    void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            DisposeUselessDoors();
            updatedDoors = true;
        }
    }


    public void DisposeUselessDoors()
    {
        foreach(Door door in doorsList)
        {
            switch (door.doorPlacemant)
            {
                case Door.DoorPlacemant.RIGHT:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorPlacemant.LEFT:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorPlacemant.TOP:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorPlacemant.BOTTOM:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public Room GetDoor(DoorPlacemant dp)
    {
        switch(dp)
        {
            case Door.DoorPlacemant.TOP:
                if (RoomsManager.instance.CheckRoom(roomMetrics.x, roomMetrics.y + 1))
                {
                    return RoomsManager.instance.FindRoom(roomMetrics.x, roomMetrics.y + 1);
                }
                break;

            case Door.DoorPlacemant.BOTTOM:
                if (RoomsManager.instance.CheckRoom(roomMetrics.x, roomMetrics.y - 1))
                {
                    return RoomsManager.instance.FindRoom(roomMetrics.x, roomMetrics.y - 1);
                }
                break;

            case Door.DoorPlacemant.LEFT:
                if (RoomsManager.instance.CheckRoom(roomMetrics.x - 1, roomMetrics.y))
                {
                    return RoomsManager.instance.FindRoom(roomMetrics.x - 1, roomMetrics.y);
                }
                break;
            case Door.DoorPlacemant.RIGHT:
                if (RoomsManager.instance.CheckRoom(roomMetrics.x + 1, roomMetrics.y))
                {
                    return RoomsManager.instance.FindRoom(roomMetrics.x + 1, roomMetrics.y);
                }
                break;
        }
        return null;
    }


    public Room GetRight()
    {
        if (RoomsManager.instance.CheckRoom(roomMetrics.x + 1, roomMetrics.y))
        {
            return RoomsManager.instance.FindRoom(roomMetrics.x + 1, roomMetrics.y);
        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomsManager.instance.CheckRoom(roomMetrics.x - 1, roomMetrics.y))
        {
            return RoomsManager.instance.FindRoom(roomMetrics.x - 1, roomMetrics.y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomsManager.instance.CheckRoom(roomMetrics.x, roomMetrics.y + 1))
        {
            return RoomsManager.instance.FindRoom(roomMetrics.x, roomMetrics.y + 1); 
        }
        return null;
    }
    public Room GetBottom()
    {
        if (RoomsManager.instance.CheckRoom(roomMetrics.x, roomMetrics.y - 1))
        {
            return RoomsManager.instance.FindRoom(roomMetrics.x, roomMetrics.y - 1);
        }
        return null;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, 
                            new Vector3(roomMetrics.width, roomMetrics.height, 0));
    }

    public Vector3 GetOrigin()
    {
        return new Vector3(this.roomMetrics.width * this.roomMetrics.x, 
                           this.roomMetrics.height * this.roomMetrics.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RoomsManager.instance.OnEntering(this);
        }
    }
}
