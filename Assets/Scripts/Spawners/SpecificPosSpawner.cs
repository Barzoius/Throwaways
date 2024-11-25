using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPosSpawner : MonoBehaviour
{
    public GridManager gridManager;

    public Vector2[] spawnPos; // Logical positions (e.g., (1,1), (5,1), etc.)
    public GameObject objectToSpawn;

    private float tolerance = 0.01f; // Tolerance for floating-point comparisons

    // Custom method to find a tile index with a tolerance
    private int FindTileIndex(Vector2 position, List<Vector2> tiles, float tolerance = 0.01f)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (Vector2.Distance(tiles[i], position) <= tolerance)
            {
                return i;
            }
        }
        return -1; // Not found
    }

    public void SpawnObjects()
    {
        foreach (var logicalPos in spawnPos)
        {
            // Convert logical grid position (row, col) to the world position used in freeTiles
            float worldX = logicalPos.x - (gridManager.grid.cols - gridManager.grid.horiOffset);
            float worldY = logicalPos.y - gridManager.grid.rows - gridManager.grid.vertOffset;
            Vector2 worldPos = new Vector2(worldX, worldY);

            // Find the corresponding tile in freeTiles
            int tileIndex = FindTileIndex(worldPos, gridManager.freeTiles, tolerance);

            if (tileIndex == -1)
            {
                Debug.LogError($"Position {logicalPos} (world {worldPos}) not found in freeTiles.");
                continue;
            }

            // Spawn the object at the correct position
            Instantiate(objectToSpawn, gridManager.freeTiles[tileIndex], Quaternion.identity, transform);

            // Remove the tile from freeTiles to mark it as occupied
            gridManager.freeTiles.RemoveAt(tileIndex);
        }
    }
}
