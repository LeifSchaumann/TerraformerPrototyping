using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelController : MonoBehaviour
{
    public GameObject blockPrefab;
    private Grid grid;
    public int levelWidth;
    public int levelHeight;
    public float density;
    public float keyChance;
    private Vector3 cellShift;
    private float lastShiftTime;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        cellShift = grid.cellSize + grid.cellGap;
        lastShiftTime = Time.time;
    }
    private void Start()
    {
        MakeSquare();
    }
    private void Update()
    {
        if (Time.time > lastShiftTime + 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ShiftColumn(2, 1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShiftColumn(2, -1);
            }
        }
    }
    private void MakeSquare()
    {
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                Instantiate(blockPrefab, grid.CellToWorld(new Vector3Int(x, y)), Quaternion.identity, transform);
            }
        }
    }
    private void ShiftColumn(int n, int shift)
    {
        if (shift == 1)
        {
            Instantiate(blockPrefab, grid.CellToWorld(new Vector3Int(n, -1)), Quaternion.identity, transform);
        }
        else if (shift == -1)
        {
            Instantiate(blockPrefab, grid.CellToWorld(new Vector3Int(n, levelHeight)), Quaternion.identity, transform);
        }
        foreach (Transform child in transform)
        {
            Block block = child.GetComponent<Block>();
            if (block)
            {
                block.SetPosition();
                Vector3Int blockGridPos = grid.WorldToCell(block.transform.position);
                if (blockGridPos.x == n)
                {
                    block.Shift(new Vector3Int(0, shift));
                    if ((shift == 1 && blockGridPos.y == levelHeight - 1) || (shift == -1 && blockGridPos.y == 0))
                    {
                        block.Fade();
                    }
                }
            }
        }
        lastShiftTime = Time.time;
    }
}
