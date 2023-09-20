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
    public float density;
    public float keyChance;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Start()
    {
        MakeSquare();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShiftBlocks(GetColumn(2), new Vector3Int(0, 1));
        }
    }
    private void MakeSquare()
    {
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelWidth; y++)
            {
                Instantiate(blockPrefab, grid.CellToWorld(new Vector3Int(x, y)), Quaternion.identity, transform);
            }
        }
    }
    private List<Block> GetColumn(int n)
    {
        List<Block> column = new List<Block>();
        foreach (Transform child in transform)
        {
            Block block = child.GetComponent<Block>();
            if (block)
            {
                if (grid.WorldToCell(block.transform.position).x == n)
                {
                    column.Add(block);
                }
            }
        }
        return column;
    }
    private void ShiftBlocks(List<Block> blocks, Vector3Int shift)
    {
        foreach (Block block in blocks)
        {
            block.Shift(shift);
        }
    }
}
