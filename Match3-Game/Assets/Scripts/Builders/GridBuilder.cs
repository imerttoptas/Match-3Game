using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private SpriteRenderer gridBackground;

    /// <summary>
    ///  Generates a grid of cells.
    /// </summary>
    /// <param name="cellList">empty list of cell to add created cells</param>
    /// <param name="gridWidth">Number of columns</param>
    /// <param name="gridHeight">Number of rows</param>
    /// <param name="offset">Distance between each cells</param> 
    public void GenerateGrid(List<Cell>cellList, int gridWidth, int gridHeight, float offset)
    {
        Vector3 cellScale = GetCalculatedCellScale(gridWidth, gridHeight);
        Vector2 firstCellPosition = gridBackground.transform.position -
                                    (gridBackground.bounds.size - cellScale )/2;
        for (int row = 0; row < gridWidth; row++)
        {
            for (int col = 0; col < gridHeight; col++)
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
        createdCell.transform.position = firstCellPosition + new Vector2(col, row) * cellScale;
        createdCell.transform.localScale = cellScale - Vector2.one*offSet;
        createdCell.Init(row,col);
        return createdCell;
    }
    /// <summary>
    /// Calculate and return the scale of each cell
    /// </summary>
    /// <param name="gridWidth">Number of row in the grid</param>
    /// <param name="gridHeight">Number of column in the grid</param>
    /// <returns>Scale of the cell</returns>
    private Vector3 GetCalculatedCellScale(int gridWidth,int gridHeight)
    {
        float cellScaleX = gridBackground.bounds.size.x / gridWidth;
        float cellScaleY = gridBackground.bounds.size.y / gridHeight;
        return new Vector3(cellScaleX, cellScaleY);
    }

    

}
