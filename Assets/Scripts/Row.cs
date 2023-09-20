using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Row : MonoBehaviour
{
    public GameObject blockPrefab;
    private Grid grid;
    private Vector3 desiredPos;
    private Vector3 spawnPoint;
    private float shiftTimer;
    private LevelController levelController;

    private void Awake()
    {
        grid = GetComponentInParent<Grid>();
        levelController = GetComponentInParent<LevelController>();
        desiredPos = transform.position;
        spawnPoint = transform.position;
        shiftTimer = 0;
    }
    private void Start()
    {
        Generate();
    }
    private void Update()
    {
        if (shiftTimer >= levelController.shiftRate)
        {
            shiftTimer = 0;
            ShiftRight();
        }
        shiftTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, desiredPos, 0.01f);
    }
    public void Generate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int x = 0; x < levelController.levelWidth; x++)
        {
            Instantiate(blockPrefab, grid.CellToWorld(grid.WorldToCell(transform.position) + Vector3Int.right * x), Quaternion.identity, transform);
        }
    }
    private void ShiftRight()
    {
        Vector3 cellShiftX = new Vector3(grid.cellSize.x + grid.cellGap.x, 0);
        Instantiate(blockPrefab, spawnPoint - (desiredPos - transform.position) - cellShiftX, Quaternion.identity, transform);
        desiredPos += cellShiftX;
    }
    public void Place()
    {
        transform.position = desiredPos;
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        Transform newParent = transform.parent;
        foreach (Transform child in children)
        {
            child.SetParent(newParent);
        }
        Destroy(gameObject);
    }
}