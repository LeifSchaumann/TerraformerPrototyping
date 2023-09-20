using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Block : MonoBehaviour
{

    private Tilemap tilemap;
    public Tile wallTile;
    public GameObject keyPrefab;
    private LevelController levelController;
    private Grid myGrid;
    private Grid levelGrid;
    private Vector3 desiredPos;
    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        myGrid = GetComponent<Grid>();
        levelGrid = transform.parent.GetComponent<Grid>();
        levelController = GetComponentInParent<LevelController>();
        desiredPos = transform.position;
    }
    private void Start()
    {
        Generate();
    }
    private void Update()
    {
        if (transform.position.y - Camera.main.transform.position.y < -20 || transform.position.x - Camera.main.transform.position.x > 25)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.Lerp(transform.position, desiredPos, 0.01f);
    }
    public void Generate()
    {
        Vector3Int[] compass = { Vector3Int.right, Vector3Int.down, Vector3Int.up, Vector3Int.left };
        foreach (Vector3Int dir in compass)
        {
            if (Random.Range(0f, 1f) <= levelController.density)
            {
                tilemap.SetTile(new Vector3Int(1, 1) + dir, wallTile);
            }
        }
        if (Random.Range(0f, 1f) <= levelController.keyChance)
        {
            Instantiate(keyPrefab, myGrid.CellToWorld(new Vector3Int(1, 1)) + myGrid.cellSize/2, Quaternion.identity, transform);
        }
    }
    public void Shift(Vector3Int shift)
    {
        Vector3 cellShift = levelGrid.cellSize + levelGrid.cellGap;
        desiredPos.x += shift.x * cellShift.x;
        desiredPos.y += shift.y * cellShift.y;
    }
}
