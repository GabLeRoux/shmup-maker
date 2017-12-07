﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Spaceship))]
public class AutoPilot : MonoBehaviour
{
    const float REACHED_THRESHOLD = 1.5f;

    Spaceship ship;
    Queue<Vector3> destinations = new Queue<Vector3>();

    void Start()
    {
        ship = GetComponent<Spaceship>();
    }

    public void QueueDestination(Vector3 destination)
    {
        destinations.Enqueue(destination);
    }

    public void OverrideDestination(Vector3 destination)
    {
        destinations.Clear();
        QueueDestination(destination);
    }

    void FixedUpdate()
    {
        if (destinations.Count > 0)
            FollowCourse();
        else
            ship.Remove();
    }

    void FollowCourse()
    {
        var currentDestination = destinations.Peek();

        ship.Engine.MoveToward(currentDestination);
        if (Vector3.Distance(transform.position, currentDestination) <= REACHED_THRESHOLD)
            destinations.Dequeue();
    }

    void Update()
    {
        ship.FirePrimaryWeapon();
    }
}