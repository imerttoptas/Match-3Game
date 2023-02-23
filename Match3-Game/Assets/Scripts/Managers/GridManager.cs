using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GridManager : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;
    [SerializeField] private MatchChecker matchChecker;
    [SerializeField] private LevelGenerator levelBuilder;
    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private float offset;
    private List<Cell> _cellList = new List<Cell>();
    private MatchType _matchType;
    // public Action<List<Cell>> OnCellsSwapped;


    void Start()
    {
        gridBuilder.GenerateGrid(_cellList,gridWidth, gridHeight, offset);
        levelBuilder.GenerateRandomLevel(_cellList);
    }
    
    /// <summary>
    /// Swaps Blocks of two cells
    /// </summary>
    /// <param name="firstCell">first Block's cell</param>
    /// <param name="secondCell">second Block's cell</param>
    public void SwapBlocks(Cell firstCell, Cell secondCell)
    {
        Transform firstCellTransform = firstCell.transform;
        Transform secondCellTransform = secondCell.transform;
        Block firstBlock = firstCell.GetChildBlock();
        Block secondBlock = secondCell.GetChildBlock();
        
        secondBlock.transform.SetParent(firstCellTransform);
        firstBlock.transform.SetParent(secondCellTransform);
        
        if (matchChecker.IsThereAMatch(firstCell,secondBlock.BlockType) || matchChecker.IsThereAMatch(secondCell,firstBlock.BlockType))
        {
            firstBlock.transform.DOMove(secondCellTransform.position, 0.5f);
            secondBlock.transform.DOMove(firstCellTransform.position, 0.5f);
        }
        else
        {
             firstBlock.transform.DOMove(secondCellTransform.position, 0.5f).OnComplete(()=>firstBlock.transform.DOMove(firstCellTransform.position,0.5f));
             secondBlock.transform.DOMove(firstCellTransform.position, 0.5f).OnComplete(()=>secondBlock.transform.DOMove(secondCellTransform.position,0.5f));
             secondBlock.transform.SetParent(secondCellTransform);
             firstBlock.transform.SetParent(firstCellTransform);
        }
    }
    
    
    private MatchType FindMatchType(List<Block> horizontal, List<Block> vertical)
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
    public Cell TryGetCellAt(int row, int col)
    {
        if (row < 0 || row >=gridWidth || col < 0 || col >= gridHeight)
        {
            return null;
        }
        
        return _cellList[row*gridWidth + col];;
    }
    
}

