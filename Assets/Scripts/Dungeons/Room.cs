using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public (int width, int height, int x, int y) roomMetrics;



    void Start()
    {
        if(RoomsManager.instance == null)
        {
            return;
        }
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
}
