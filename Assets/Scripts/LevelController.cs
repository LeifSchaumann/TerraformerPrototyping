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
            foreach (Transform child in transform)
            {
                Block block = child.GetComponent<Block>();
                if (block != null )
                {
                    block.Shift(new Vector3Int(0, 1));
                }
            }
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
}
