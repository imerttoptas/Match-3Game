using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;
    [SerializeField] private int gridSizeX, gridSizeY;
    [SerializeField] private float offset;

    private List<Cell> cellList = new List<Cell>();
    void Start()
    {
        gridBuilder.GenerateGrid(cellList,gridSizeX, gridSizeY, offset);
    }
    
}
