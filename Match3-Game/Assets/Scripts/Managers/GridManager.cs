using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstCell"></param>
    /// <param name="secondCell"></param>
    public void SwapMatchObjects(Cell firstCell, Cell secondCell)
    {
        Vector3 tempPos = firstCell.transform.position;
        Transform firstCellTransform = firstCell.transform;
        GameObject firstMatchObject = firstCell.transform.GetChild(0).gameObject;
        GameObject secondMatchObject = secondCell.transform.GetChild(0).gameObject;

        firstMatchObject.transform.position = secondCell.transform.position;
        secondMatchObject.transform.position = tempPos;
        firstMatchObject.transform.SetParent(secondCell.transform);
        secondMatchObject.transform.SetParent(firstCellTransform);
    }
}

