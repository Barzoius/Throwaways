using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Room room;

    Queue<RoomInfo> roomQueue = new Queue<RoomInfo>();

    public List<Room> rooms = new List<Room>();

    bool isLoaded = false;

    bool hasBossRoom = false;
    bool hasChestRoom = false;

    bool updatedRooms = false;

    public bool CheckRoom(int x, int y)
    {
        return rooms.Find(item => item.roomMetrics.x == x && item.roomMetrics.y == y) != null;
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

        if (!CheckRoom(roomInfo.roomIndex.x, roomInfo.roomIndex.y)) // to prevent overlapping
        {

            room.transform.position = new Vector3(roomInfo.roomIndex.x * room.roomMetrics.width,
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
        else
        {
            Destroy(room.gameObject);
            isLoaded = false ;
        }
    }

  

    void Start()
    {

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

        if ( roomQueue.Count == 0)
        {
            if(!hasBossRoom)
            {
                StartCoroutine(CreateBossRoom());
            }
            else if(hasBossRoom && !updatedRooms)
            {
                foreach(Room room in rooms)
                {
                    room.DisposeUselessDoors();
                }

                updatedRooms = true;
            }
            return; 
        }

        roomInfo = roomQueue.Dequeue();
        isLoaded = true;

        StartCoroutine(LoadRooomRoutine(roomInfo));

    }

    private IEnumerator CreateBossRoom()
    {
        hasBossRoom = true;

        yield return new WaitForSeconds(0.5f);

        if(roomQueue.Count == 0)
        {
            if (rooms.Count > 0) // Check to ensure there is at least one room in the list
            {
                Room bossRoom = rooms[^1];
                //Room template = new Room(bossRoom.roomMetrics.x, bossRoom.roomMetrics.y);
                Vector2Int template = new Vector2Int(bossRoom.roomMetrics.x, bossRoom.roomMetrics.y);
                Destroy(bossRoom.gameObject);

                var removedRoom = rooms.Single(r => r.roomMetrics.x == template.x &&
                                                    r.roomMetrics.y == template.y);

                rooms.Remove(removedRoom);

                LoadRoom("End", template.x, template.y);
            }

        }
    }

    public void OnEntering(Room pRoom)
    {
        CameraManager.instance.room = pRoom;
        room = pRoom;
    }


    public Room FindRoom(int x, int y)
    {
        return rooms.Find(item => item.roomMetrics.x == x && item.roomMetrics.y == y);
    }

    public string GetRandomRomType()
    {
        string[] roomTypes = new string[]
        {
            "Empty",
            "Type1"
        };

        return roomTypes[Random.Range(0, roomTypes.Length)];
    }

}
