using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrawlDirection
{
    TOP = 0, BOTTOM = 1, LEFT = 2, RIGHT = 3
};

public class CrawlerManager : MonoBehaviour
{
    public static List<Vector2Int> Discovered = new List<Vector2Int>();

    private static Dictionary<CrawlDirection, Vector2Int> dirMap = new Dictionary<CrawlDirection, Vector2Int>
    {
        {CrawlDirection.TOP, Vector2Int.up},
        {CrawlDirection.BOTTOM, Vector2Int.down},
        {CrawlDirection.LEFT, Vector2Int.left},
        {CrawlDirection.RIGHT, Vector2Int.right}
    };

    public static List<Vector2Int> Generate(GenerationMetrics metrics)
    {
        List<Crawler> crawlersList = new List<Crawler>();

        for(int i = 0; i < metrics.numCrawlers; i++)
        {
            crawlersList.Add(new Crawler(Vector2Int.zero));
        }

        int iterations = Random.Range(metrics.minIteration, metrics.maxIteration);

        for(int i = 0; i < iterations; i++)
        {
            foreach(Crawler crawler in crawlersList)
            {
                Vector2Int newPos = crawler.Placement(dirMap);
                Discovered.Add(newPos);
            }
        }

        return Discovered;
    }


        
   
}
