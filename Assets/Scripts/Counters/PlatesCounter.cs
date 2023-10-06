using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;
    
    [SerializeField] private KitchenObjectSO PlatesSo;
    
    private float _spawnPlateTimer;
    private const float SpawnFrequency = 4f;
    private int _platesSpawnedAmount;
    private int _plateSpawnerAmountMax = 4;

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if (_spawnPlateTimer > SpawnFrequency)
        {
            _spawnPlateTimer = 0f;
            if (_platesSpawnedAmount < _plateSpawnerAmountMax)
            {
                _platesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(_platesSpawnedAmount > 0)
            {
                _platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(PlatesSo, player);
                OnPlateTaken?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
