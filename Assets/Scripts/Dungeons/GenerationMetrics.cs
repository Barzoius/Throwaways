using UnityEngine;

[CreateAssetMenu(fileName = "GenerationMetrics.asset", menuName = "GenerationMetrics/Metrics")]
public class GenerationMetrics : ScriptableObject
{
    public int numCrawlers;

    public int minIteration;
    public int maxIteration;


}
