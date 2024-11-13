using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GenerationMetrics metrics;

    private List<Vector2Int> roomsList;

    private void Start()
    {
        roomsList = CrawlerManager.Generate(metrics);
        CreateRooms(roomsList);
    }

    private void CreateRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomsManager.instance.LoadRoom("Start", 0, 0);

        foreach(Vector2Int room in rooms)
        {
            RoomsManager.instance.LoadRoom(RoomsManager.instance.GetRandomRomType(), room.x, room.y);
            
        }
    }
}
