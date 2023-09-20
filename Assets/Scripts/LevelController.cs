using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelController : MonoBehaviour
{
    public GameObject rowPrefab;
    private Grid grid;
    public int levelWidth;
    private int currentRow;
    public float density;
    public float keyChance;
    public float shiftRate;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        currentRow = 0;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextRow();
        }
    }
    public void NextRow()
    {
        if (GetComponentInChildren<Row>())
        {
            GetComponentInChildren<Row>().Place();
        }
        Instantiate(rowPrefab, grid.CellToWorld(new Vector3Int(0, currentRow)), Quaternion.identity, transform);
        currentRow++;
        Camera.main.GetComponent<CameraMovement>().desiredPos += new Vector3(0, grid.cellSize.y + grid.cellGap.y);
    }
}
