using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private int rowNumber;
    private int colNumber;
    
    public int RowNumber { get { return rowNumber; } }
    public int ColNumber { get { return colNumber; } }
    /// <summary>
    /// Initialize the cell with the provided row and column numbers.
    /// </summary>
    /// <param name="_rowNumber">The row number of the cell</param>
    /// <param name="_colNumber">The column number of the cell</param>
    public void Init(int _rowNumber, int _colNumber)
    {
        rowNumber = _rowNumber;
        colNumber = _colNumber;
        name = $"Cell{rowNumber}x{colNumber}";
    }
}
