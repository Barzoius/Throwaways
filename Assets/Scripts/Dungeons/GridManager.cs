using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Room room;

    [System.Serializable]
    public struct Grid
    {
        public int rows, cols;
        public float vertOffset, horiOffset;
    }

    public Grid grid;

    public GameObject gridTile; 

    public List<Vector2> freeTiles = new List<Vector2>();

    private void Awake()
    {
        room = GetComponentInParent<Room>();

        //grid.cols = (int)room.roomMetrics.width - 1;
        //grid.rows = (int)room.roomMetrics.height - 1;

        CreateGrid();

    }



    public void CreateGrid()
    {
        //grid.vertOffset += room.transform.localPosition.y;
        //grid.horiOffset += room.transform.localPosition.x;

        for (int i = 0; i < grid.rows; i++)
        {
            for (int j = 0; j < grid.cols; j++)
            {
                GameObject go = Instantiate(gridTile, transform);

                go.GetComponent<Transform>().position
                    = new Vector2((float)j - (grid.cols - grid.horiOffset),
                                   (float)i - (grid.rows) - grid.vertOffset);

                go.name = "X: " + j + "," + "Y: " + i;
                freeTiles.Add(go.transform.position);

                go.SetActive(false);
            }
        }


        var objectRoomSpawner = GetComponentInParent<ObjectRoomSpawner>();
        if (objectRoomSpawner != null)
        {
            objectRoomSpawner.InitialiseObjectSpawners();
            return; 
        }

        var specificPosSpawner = GetComponentInParent<SpecificPosSpawner>();
        if (specificPosSpawner != null)
        {
            specificPosSpawner.SpawnObjects();
            return; 
        }

        var chestSpawner = GetComponentInParent<ChestSpawner>();
        if (chestSpawner != null)
        {
            chestSpawner.SpawnObjects();
            return; 
        }
    }
}
