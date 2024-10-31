using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{

    public Vector2Int position { get; set; }

    public Crawler(Vector2Int pos)
    {
         position = pos;
    }

    public Vector2Int Placement(Dictionary<CrawlDirection, Vector2Int> dirMap)
    {
        CrawlDirection placmentPos = (CrawlDirection)Random.Range(0, dirMap.Count);
        position += dirMap[placmentPos];
        return position;
    }

}
