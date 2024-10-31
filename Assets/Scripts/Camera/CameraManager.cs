using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Room room;

    [SerializeField]
    private float transitionSpeed;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (room == null) { return; }

        Vector3 target = GetTarget();

        transform.position = Vector3.MoveTowards(transform.position,
                                                 target,
                                                 Time.deltaTime * transitionSpeed);
    }

    Vector3 GetTarget()
    {
        if (room == null) { return Vector3.zero; }

        Vector3 target = room.GetOrigin();

        target.z = transform.position.z;

        return target;

    }


    public bool EnteredRoom()
    {
        return transform.position.Equals(GetTarget()) == false;
    }

}

