using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;
    [SerializeField] private LevelGenerator levelBuilder;
    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private float offset;
    public List<Cell> cellList = new List<Cell>();
    void Start()
    {
        gridBuilder.GenerateGrid(cellList,gridWidth, gridHeight, offset);
        levelBuilder.GenerateRandomLevel(cellList);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstCell"></param>
    /// <param name="secondCell"></param>
    public void SwapMatchObjects(Cell firstCell, Cell secondCell)
    {
        Transform tempTransform = firstCell.transform;
        GameObject firstMatchObject = firstCell.transform.GetChild(0).gameObject;
        GameObject secondMatchObject = secondCell.transform.GetChild(0).gameObject;

        firstMatchObject.transform.position = secondCell.transform.position;
        firstMatchObject.transform.SetParent(secondCell.transform);
        secondMatchObject.transform.position = tempTransform.position;
        secondMatchObject.transform.SetParent(tempTransform);
    }
    /// <summary>
    /// Returning the neighbour cells of a given cell
    /// </summary>
    /// <param name="cell">Cell to get neighbours</param>
    /// <returns>List of neighbour cells</returns>
    public List<Cell> GetNeighbourCells(Cell cell)
    {
        List<Cell> neighbourCells = new List<Cell>();

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (Mathf.Abs(i) != Mathf.Abs(j))
                {
                    var neighbour = TryToGetCell(cell.RowNumber + i, cell.ColNumber + j);
                    if (neighbour)
                    {
                        neighbourCells.Add(neighbour);
                    }
                }
            }
        }
        
        return neighbourCells;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns> 
    private Cell TryToGetCell(int row, int col)
    {
        if (row < 0 || row >=gridWidth || col < gridHeight || col >= gridHeight )
        {
            return cellList[row*gridWidth + col];
        }

        return null;
    }
}

