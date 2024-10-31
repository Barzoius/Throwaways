using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GenerationMetrics metrics;

    private List<Vector2Int> rooms;

    private void Start()
    {
        rooms = CrawlerManager.Generate(metrics);
        CreateRooms(rooms);
    }

    private void CreateRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomsManager.instance.LoadRoom("Start", 0, 0);

        foreach(Vector2Int room in rooms)
        {
            RoomsManager.instance.LoadRoom("Empty", room.x, room.y);
        }
    }
}
