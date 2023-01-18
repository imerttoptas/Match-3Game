using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private SpriteRenderer gridBackground;
    
     /// <summary>
     ///  Generates a grid of cells.
     /// </summary>
     /// <param name="gridSizeX">Number of columns</param>
     /// <param name="gridSizeY">Number of rows</param>
     /// <param name="offset">Distance between each cells</param> 
    public void GenerateGrid(List<Cell>cellList, int gridSizeX, int gridSizeY, float offset)
    {
        Vector3 cellScale = GetCalculatedCellScale(gridSizeX, gridSizeY);
        Vector2 firstCellPosition = gridBackground.transform.position -
                                    (gridBackground.bounds.size - cellScale )/2;
        for (int row = 0; row < gridSizeX; row++)
        {
            for (int col = 0; col < gridSizeY; col++)
            {
                Cell cell = GetCreatedCell(row,col,firstCellPosition,cellScale,offset);
                cellList.Add(cell);
            }
        }
    }
    /// <summary>
    ///  Create a new cell at the specified position and scale 
    /// </summary>
    /// <param name="row">The row of the cell</param>
    /// <param name="col">The column of the cell</param>
    /// <param name="firstCellPosition">The position of the first cell</param>
    /// <param name="cellScale">The scale of the cell</param>
    /// <param name="offSet">Distance between cell</param>
    /// <returns>created cell</returns>
    
    private Cell GetCreatedCell(int row, int col, Vector2 firstCellPosition, Vector2 cellScale, float offSet)
    {
        
        Cell createdCell = Instantiate(cellPrefab, gridBackground.transform, true);
        createdCell.transform.position = firstCellPosition + new Vector2(row, col) * cellScale;
        createdCell.transform.localScale = cellScale - Vector2.one*offSet;
        createdCell.name = "cell" + row + "x" + col;
        return createdCell;
    }
    /// <summary>
    /// Calculate and return the scale of each cell
    /// </summary>
    /// <param name="gridSizeX">Number of row in the grid</param>
    /// <param name="gridSizeY">Number of column in the grid</param>
    /// <returns>Scale of the cell</returns>
    private Vector3 GetCalculatedCellScale(int gridSizeX,int gridSizeY)
    {
        float cellScaleX = gridBackground.bounds.size.x / gridSizeX;
        float cellScaleY = gridBackground.bounds.size.y / gridSizeY;
        return new Vector3(cellScaleX, cellScaleY);
    }
    
}
