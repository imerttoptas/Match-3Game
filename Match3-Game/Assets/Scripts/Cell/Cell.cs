using UnityEngine;

public class Cell : MonoBehaviour
{
    private int _rowNumber;
    private int _colNumber;
    
    public int RowNumber => _rowNumber;
    public int ColNumber => _colNumber;

    public CellType cellType;
    
    /// <summary>
    /// Initialize the cell with the provided row and column numbers.
    /// </summary>
    /// <param name="rowNumber">The row number of the cell</param>
    /// <param name="colNumber">The column number of the cell</param>
    public void Init(int rowNumber, int colNumber)
    {
        _rowNumber = rowNumber;
        _colNumber = colNumber;
        name = $"Cell{_rowNumber}x{_colNumber}";
    }
}
