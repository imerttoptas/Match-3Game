using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;
    [SerializeField] private LevelGenerator levelBuilder;
    [SerializeField] private int gridSizeX, gridSizeY;
    [SerializeField] private float offset;

    public List<Cell> cellList = new List<Cell>();
    void Start()
    {
        gridBuilder.GenerateGrid(cellList,gridSizeX, gridSizeY, offset);
        levelBuilder.GenerateRandomLevel(cellList);
    }
}

