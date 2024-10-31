using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public enum DoorPlacemant
    { 
        TOP, BOTTOM, LEFT, RIGHT
    }

    public enum DoorType
    {
        NORMAL_OPENED, NORMAL_CLOSED, CHEST, BOOS 
    }

    public DoorPlacemant doorPlacemant;
}
