using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;


public class GridManager : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;
    [SerializeField] private LevelGenerator levelBuilder;
    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private float offset;
    private List<Cell> _cellList = new List<Cell>();
    private MatchType _matchType;
    private System.Action<Cell> OnSwappedCell;

    void Start()
    {
        gridBuilder.GenerateGrid(_cellList,gridWidth, gridHeight, offset);
        levelBuilder.GenerateRandomLevel(_cellList);
    }
    
    /// <summary>
    /// Swaps MatchObjects of two cells
    /// </summary>
    /// <param name="firstCell">first MatchObject's cell</param>
    /// <param name="secondCell">second MatchObject's cell</param>
    public void SwapMatchObjects(Cell firstCell, Cell secondCell)
    {
        Transform tempTransform = firstCell.transform;
        MatchObject firstMatchObject = firstCell.GetChildMatchObject();
        MatchObject secondMatchObject = secondCell.GetChildMatchObject();

        firstMatchObject.transform.DOMove(secondCell.transform.position, 0.5f);
        firstMatchObject.transform.SetParent(secondCell.transform);
        secondMatchObject.transform.DOMove(tempTransform.position, 0.5f);
        secondMatchObject.transform.SetParent(tempTransform);
    }
    
    
    /// <summary>
    /// Checks for possible matches for the given cell
    /// </summary>
    /// <param name="cell">Cell to check possible matches</param>
    /// <returns>True if match is found, otherwise false</returns>
    public bool CheckForMatches(Cell cell)
    {
        MatchObjectType targetType = cell.GetChildMatchObject().MatchObjectType;
        bool isHorizontalMatch = false;
        bool isVerticalMatch = false;
        
        List<Cell> verticalCells = GetMatchingCellsInDirection(cell, new Vector2(1, 0), targetType)
            .Concat(GetMatchingCellsInDirection(cell, new Vector2(-1, 0), targetType)).ToList();
        List<Cell> horizontalCells = GetMatchingCellsInDirection(cell, new Vector2(0, 1), targetType)
            .Concat(GetMatchingCellsInDirection(cell, new Vector2(0, -1), targetType)).ToList();
        
        if (verticalCells.Count>=2)
        { 
            isHorizontalMatch = true;
        }
        if (horizontalCells.Count>=2)
        {
            isVerticalMatch = true;
        }

        if (isHorizontalMatch || isVerticalMatch)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// This function returns list of MatchObject, that matches with the given cell type in the given direction.
    /// </summary>
    /// <param name="cell">Cell to look for matchs</param>
    /// <param name="direction">Direction to look for cells</param>
    /// <param name="targetMatchObjectType">Match object type for Match control</param>
    /// <param name="maxSteps">Number of cells to look in the given direction</param>
    /// <returns>the list of matching cells found in the given direction.</returns>
    private List<Cell> GetMatchingCellsInDirection(Cell cell, Vector2 direction,MatchObjectType targetMatchObjectType,int maxSteps = 2)
    {
        List<Cell> matchingCells = new List<Cell>();
        int rowStep = (int)direction.x;
        int colStep = (int)direction.y;
        Cell nextCell = TryGetCellAt(cell.RowNumber + rowStep, cell.ColNumber + colStep);
        int steps = 0;
        while (IsMatchingCell(nextCell, targetMatchObjectType) && steps < maxSteps)
        {
            matchingCells.Add(nextCell);
            nextCell = TryGetCellAt(nextCell.RowNumber + rowStep, nextCell.ColNumber + colStep);
            steps++;
        }
        return matchingCells;
    }
    /// <summary>
    /// Checks if the the given Cell's MatchObjectType is same with the given MatchObjectType 
    /// </summary>
    /// <param name="cell">Cell to compare MatchObjectType</param>
    /// <param name="matchObjectType">target MatchObjectType </param>
    /// <returns>True if the MatchObjectType of Cell is same with given MatchObjectType, otherwise false </returns>
    private bool IsMatchingCell(Cell cell,MatchObjectType matchObjectType)
    {
        return cell && cell.GetChildMatchObject().MatchObjectType == matchObjectType;
    }
        
    private MatchType FindMatchType(List<MatchObject> horizontal, List<MatchObject> vertical)
    {
        return _matchType;
    }
    /// <summary>
    /// Returning the neighbour cells of a given cell
    /// </summary>
    /// <param name="cell">Cell to get neighbours</param>
    /// <returns>List of neighbour cells</returns>
    private List<Cell> GetNeighbourCells(Cell cell)
    {
        List<Cell> neighbourCells = new List<Cell>();
        
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (Mathf.Abs(i) != Mathf.Abs(j))
                {
                    var neighbour = TryGetCellAt(cell.RowNumber + i, cell.ColNumber + j);
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
    /// Gets a Cell object from the cellList collection based on a specified row and col info.
    /// </summary>
    /// <param name="row">row number of desired cell </param>
    /// <param name="col">col number of desired cell</param>
    /// <returns>Returns a Cell object if the row and col values are within the grid bounds.
    /// Otherwise, it returns null.</returns>
    private Cell TryGetCellAt(int row, int col)
    {
        if (row < 0 || row >=gridWidth || col < 0 || col >= gridHeight)
        {
            return null;
        }
        
        return _cellList[row*gridWidth + col];;
    }
}

