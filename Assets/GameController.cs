﻿using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] Vehicle playerShipPrefab;
    [SerializeField] Theatre theatre;

    Action executeState;
    IDictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();
    Level currentLevel;

    void Start()
    {
        executeState = PlayerRegistrationState;
    }

    void Update()
    {
        executeState();
    }

    void AddPlayer(int id)
    {
        var ship = Instantiate<Vehicle>(playerShipPrefab);
        var controller = ship.gameObject.AddComponent<PlayerController>();
        controller.ControllerId = id;
        players.Add(id, controller);
    }

    void PlayerRegistrationState()
    {
        for (int i = 0; i < 5; i++)
            if (!players.ContainsKey(i) && Input.GetButtonDown(string.Format("P{0}Action1", i)))
                AddPlayer(i);

        if (players.Count > 0 && Input.GetButtonDown("Next"))
            BeginLevel();
    }

    void BeginLevel()
    {
        currentLevel = FindObjectOfType<Level>();
        if (currentLevel == null)
        {
            Debug.LogError("NO LEVEL FOUND!");
            return;
        }

        currentLevel.Begin();
        executeState = delegate
        {
        };
    }
}
