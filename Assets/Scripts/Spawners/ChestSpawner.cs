using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GridManager gridManager;

    public Vector2 spawnPos; // logical positions 
    public GameObject objectToSpawn;

    private float tolerance = 0.01f; // tolerance for floating-point comparisons

    //method to find a tile index with a tolerance
    private int FindTileIndex(Vector2 position, List<Vector2> tiles, float tolerance = 0.01f)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (Vector2.Distance(tiles[i], position) <= tolerance)
            {
                return i;
            }
        }
        return -1; // not found
    }

    public void SpawnObjects()
    {
            // convert logical grid position (row, col) to the world position used in freeTiles
            float worldX = spawnPos.x - (gridManager.grid.cols - gridManager.grid.horiOffset);
            float worldY = spawnPos.y - gridManager.grid.rows - gridManager.grid.vertOffset;
            Vector2 worldPos = new Vector2(worldX, worldY);

            //fFind the corresponding tile in freeTiles
            int tileIndex = FindTileIndex(worldPos, gridManager.freeTiles, tolerance);

            if (tileIndex == -1)
            {
                Debug.LogError($"Position {spawnPos} (world {spawnPos}) not found in freeTiles.");
            }

            // spawn the object at the correct position
            Instantiate(objectToSpawn, gridManager.freeTiles[tileIndex], Quaternion.identity, transform);

            // remove the tile from freeTiles 
            gridManager.freeTiles.RemoveAt(tileIndex);
        
    }
}
