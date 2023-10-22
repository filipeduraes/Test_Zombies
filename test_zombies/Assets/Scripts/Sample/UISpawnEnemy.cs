﻿using System.Collections;
using System.Collections.Generic;
using Quantum;
using UnityEngine;

public class UISpawnEnemy : MonoBehaviour
{
    [SerializeField] private EntityPrototypeAsset enemyPrototype = null;

    private PlayerRef _playerRef;

    public void Initialize(PlayerRef playerRef)
    {
        _playerRef = playerRef;
    }
    public void SpawnEnemy()
    {
        CommandSpawnEnemy command = new CommandSpawnEnemy()
        {
            enemyPrototypeGUID = enemyPrototype.Settings.Guid.Value,
        };
        QuantumRunner.Default.Game.SendCommand(command);
    }
}
