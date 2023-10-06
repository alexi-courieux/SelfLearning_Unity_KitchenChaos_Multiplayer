using System;
using System.Collections.Generic;
using UnityEngine;

namespace Counters
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisualPrefab;
        [SerializeField] private float plateVisualOffset = 0.1f;
        
        private Stack<GameObject> plateVisualGOList;

        private void Awake()
        {
            plateVisualGOList = new Stack<GameObject>();
        }

        private void Start()
        {
            platesCounter.OnPlateSpawned += OnPlateSpawned;
            platesCounter.OnPlateTaken += OnPlateTaken;
        }

        private void OnPlateSpawned(object sender, EventArgs e)
        {
            var plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
            plateVisualTransform.localPosition = new Vector3(0,plateVisualOffset * plateVisualGOList.Count, 0);
            plateVisualGOList.Push(plateVisualTransform.gameObject);
        }

        private void OnPlateTaken(object sender, EventArgs e)
        {
            var plateVisualGO = plateVisualGOList.Pop();
            Destroy(plateVisualGO);
        }
    }
}