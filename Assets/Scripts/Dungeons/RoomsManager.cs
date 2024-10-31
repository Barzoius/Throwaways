using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

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

    Room room;

    Queue<RoomInfo> roomQueue = new Queue<RoomInfo>();

    public List<Room> rooms = new List<Room>();

    bool isLoaded = false;

    public bool CheckRoom(int x, int y)
    {
        return rooms.Find(item => item.roomMetrics.x == x && item.roomMetrics.x == y);
    }

    public void LoadRoom(string name, int x, int y)
    {
        if(CheckRoom(x, y) == true)
        {
            return;
        }

        RoomInfo newRoom = new RoomInfo();  
        newRoom.name = name;
        newRoom.roomIndex.x = x;
        newRoom.roomIndex.y = y;

        roomQueue.Enqueue(newRoom);

    }

    IEnumerator LoadRooomRoutine(RoomInfo info)
    { 
        string roomName = worldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false) { yield return null; }


    }

    public void RegisterRoom(Room room)
    {


        room.transform.position = new Vector3(roomInfo.roomIndex.x *  room.roomMetrics.width,
                                              roomInfo.roomIndex.y * room.roomMetrics.height, 0);

        room.roomMetrics.x = roomInfo.roomIndex.x;
        room.roomMetrics.y = roomInfo.roomIndex.y;
        room.name = worldName + "//" + roomInfo.name + "// [ " 
                              + room.roomMetrics.x + " , " 
                              + room.roomMetrics.y + " ]";

        room.transform.parent = transform;

        isLoaded = false;

        if (rooms.Count == 0)
        {
            CameraManager.instance.room = room;
        }

        rooms.Add(room); 
    }

  

    void Start()
    {
        LoadRoom("Start", 0, 0);

        LoadRoom("Empty", 1, 0);

        LoadRoom("Empty", -1, 0);

        LoadRoom("Empty", 0, 1);

        LoadRoom("Empty", 0, -1);

        LoadRoom("Empty", 0, -2);


    }

    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        UpdateQueue();
    }

    private void UpdateQueue()
    {
        if (isLoaded) { return; }

        if ( roomQueue.Count == 0) {  return; }

        roomInfo = roomQueue.Dequeue();
        isLoaded = true;

        StartCoroutine(LoadRooomRoutine(roomInfo));

    }

    public void OnEntering(Room pRoom)
    {
        CameraManager.instance.room = pRoom;
        room = pRoom;
    }

}
