using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

class RoomInfo
{
    public string name;
    public (int x, int y) roomIndex;
}

public class RoomsManager : MonoBehaviour
{

    public static RoomsManager instance;

    string worldName = "Midden";

    RoomInfo roomInfo;

    Queue<RoomInfo> roomQueue = new Queue<RoomInfo>();

    public List<Room> rooms = new List<Room>();

    bool isLoaded = false;

    public bool CheckRoom(int x, int y)
    {
        return rooms.Find(item => item.roomMetrics.x == x && item.roomMetrics.x == y);
    }


    void Awake()
    {
        instance = this;
    }

   
    
}
