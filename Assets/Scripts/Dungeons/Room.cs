using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public RoomMetrics roomMetrics = new RoomMetrics();

    void Start()
    {
        if(RoomsManager.instance == null)
        {
            return;
        }

        RoomsManager.instance.RegisterRoom(this);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, 
                            new Vector3(roomMetrics.width, roomMetrics.height, 0));
    }

    public Vector3 GetOrigin()
    {
        return new Vector3(roomMetrics.width * roomMetrics.x, 
                           roomMetrics.height * roomMetrics.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RoomsManager.instance.OnEntering(this);
        }
    }
}
