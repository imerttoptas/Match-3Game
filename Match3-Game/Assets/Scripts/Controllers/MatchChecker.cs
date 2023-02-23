using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    
    [SerializeField] private GridManager _gridManager;
    public bool IsThereAMatch(Cell cell, BlockType blockType)
    {
        return GetVerticalMatches(cell, blockType).Count >= 2 || GetHorizontalMatches(cell, blockType).Count >= 2;
    }
    
    public List<Cell> GetAllMatches(Cell cell)
    {
        BlockType targetType = cell.GetChildBlock().BlockType;
        List<Cell> horizontalCells = GetMatchingCellsInDirection(cell, new Vector2(0, 1), targetType)
            .Concat(GetMatchingCellsInDirection(cell, new Vector2(0, -1), targetType)).ToList();
        List<Cell> verticalCells = GetMatchingCellsInDirection(cell, new Vector2(1, 0), targetType)
            .Concat(GetMatchingCellsInDirection(cell, new Vector2(-1, 0), targetType)).ToList();
        

        return verticalCells.Concat(horizontalCells).ToList();
    }

     private List<Cell> GetVerticalMatches(Cell cell, BlockType targetType)
     {
         List<Cell> verticalCells = GetMatchingCellsInDirection(cell, new Vector2(1, 0), targetType)
             .Concat(GetMatchingCellsInDirection(cell, new Vector2(-1, 0), targetType)).ToList();

         return verticalCells;
     }
     
     private List<Cell> GetHorizontalMatches(Cell cell, BlockType targetType)
     {
         List<Cell> horizontalCells = GetMatchingCellsInDirection(cell, new Vector2(0, 1), targetType)
             .Concat(GetMatchingCellsInDirection(cell, new Vector2(0, -1), targetType)).ToList();

         return horizontalCells;
     }
     
     /// <summary>
     /// This function returns list of Cell, that matches with the given cell type in the given direction.
     /// </summary>
     /// <param name="cell">Cell to look for matches</param>
     /// <param name="direction">Direction to look for cells</param>
     /// <param name="targetBlockType">Block type for Match control</param>
     /// <param name="maxSteps">Number of cells to look in the given direction</param>
     /// <returns>the list of matching cells found in the given direction.</returns>
     private List<Cell> GetMatchingCellsInDirection(Cell cell, Vector2 direction,BlockType targetBlockType,int maxSteps = 2)
     {
        List<Cell> matchingCells = new List<Cell>();
        int rowStep = (int)direction.x;
        int colStep = (int)direction.y;
        Cell nextCell = _gridManager.TryGetCellAt(cell.RowNumber + rowStep, cell.ColNumber + colStep);
        int steps = 0;
        while (IsCellMatching(nextCell, targetBlockType) && steps < maxSteps)
        {
            matchingCells.Add(nextCell);
            nextCell = _gridManager.TryGetCellAt(nextCell.RowNumber + rowStep, nextCell.ColNumber + colStep);
            steps++;
        }
        return matchingCells;
     }
    /// <summary>
    /// Checks if the the given Cell's BlockType is same with the given targetBlockType 
    /// </summary>
    /// <param name="cell">Cell to compare BLockType</param>
    /// <param name="targetBlockType">target BLockType </param>
    /// <returns>True if the BlockType of Cell is same with given BlockType, otherwise false </returns>
    
    private bool IsCellMatching(Cell cell, BlockType targetBlockType)
    {
        if (cell && cell.GetChildBlock())
        {
            return cell.GetChildBlock().BlockType == targetBlockType;
        }
        
        return false;

    }

    
}
