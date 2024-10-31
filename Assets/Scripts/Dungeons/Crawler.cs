using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{

    public Vector2Int Position { get; set; }

    public Crawler(Vector2Int pos)
    {
         Position = pos;
    }

    public Vector2Int Placement(Dictionary<CrawlDirection, Vector2Int> dirMap)
    {
        CrawlDirection placmentPos = (CrawlDirection)Random.Range(0, dirMap.Count);
        Position += dirMap[placmentPos];
        return Position;
    }

}
