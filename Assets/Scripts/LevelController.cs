using System.Collections;
using System.Collections.Generic;
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
}
